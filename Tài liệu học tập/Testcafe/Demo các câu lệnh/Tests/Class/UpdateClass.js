import ClassFunction from "../../Functions/ClassFunction";
import ClassSelector from "../../Selectors/ClassSelector";

const classSelector = new ClassSelector();
const classFunc = new ClassFunction();

var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);
var randomDescription = classFunc.GetRandomString(10);

fixture`Update Class`
  .page(classSelector.url)
  .beforeEach(async (t) => {
    await classFunc.CreateClass(randomName, randomDescription);
  })
  .afterEach(async (t) => {
    await classFunc.ReturnHome();
    await classFunc.DeleteClass(randomDescription);
  });

test("Check required fields ", async (t) => {
  await classFunc.UpdateClass(randomDescription, "", "");
  var isRequired = classSelector.InputEditName.hasAttribute("required");
  await t.expect(isRequired).eql(true);
  var isRequired = classSelector.InputEditDescription.hasAttribute("required");
  await t.expect(isRequired).eql(true);
});
test("Edit a duplicate name Class ", async (t) => {
  await classFunc.CreateClass(randomName + "_1", randomDescription + "_1");
  await classFunc.UpdateClass(
    randomDescription,
    randomName + "_1",
    randomDescription
  );
  var toastMessage = classSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Duplicate class");
  await classFunc.ReturnHome();
  await classFunc.DeleteClass(randomName + "_1");
});
test("Edit a valid name", async (t) => {
  await classFunc.UpdateClass(
    randomDescription,
    randomName + "_1",
    randomDescription
  );
  var toastMessage = classSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Update success.");
});
test("Edit a valid description", async (t) => {
  await classFunc.UpdateClass(
    randomDescription,
    randomName,
    randomDescription + "_1"
  );
  var toastMessage = classSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Update success.");
});
test("Edit a valid name and description", async (t) => {
  await classFunc.UpdateClass(
    randomDescription,
    randomName + "_1",
    randomDescription + "_1"
  );
  var toastMessage = classSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Update success.");
});
test("Not change", async (t) => {
  await t.click(classSelector.editButton).click(classSelector.ButtonEditSave)
  .click(classSelector.ButtonCloseToast);
  var toastMessage = classSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql('Not thing has change');
});
test("Back to list", async (t) => {
  var before = await t.getCurrentWindow();
  await t.click(classSelector.editButton).click(classSelector.ButtonEditBack);
  var after = await t.getCurrentWindow();
  await t.expect(before).eql(after);
});
test("Update a valid class with space before name", async (t) => {
  await classFunc.UpdateClass(randomDescription, "          " + randomName, randomDescription);;
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Not thing has change");
});

test("Update a valid class with space after name", async (t) => {
  await classFunc.UpdateClass(randomDescription, randomName + "          ", randomDescription);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Not thing has change");
});
test("Update a valid class with space before description", async (t) => {
  await classFunc.UpdateClass(randomDescription, randomName, "          " + randomDescription);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Not thing has change");
});

test("Update a valid class with space after description", async (t) => {
  await classFunc.UpdateClass(randomDescription, randomName, randomDescription + "          ");
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Not thing has change");
});


