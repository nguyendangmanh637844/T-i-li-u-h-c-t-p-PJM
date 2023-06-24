import ClassFunction from "../../Functions/ClassFunction";
import ClassSelector from "../../Selectors/ClassSelector";
const classFunc = new ClassFunction();
const classSelector = new ClassSelector();
fixture`Create a class`.page(classSelector.url);

var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);
var randomDescription = classFunc.GetRandomString(10);

test("Create a class with null fields", async (t) => {
  await classFunc.CreateClass("", "");
  await t
    .expect(classSelector.MessageValidateName.textContent)
    .eql("The Name field is required.");
  await t
    .expect(classSelector.MessageValidateDescription.textContent)
    .eql("The Description field is required.");
});

test("Create a class with null name field", async (t) => {
  await classFunc.CreateClass("", randomDescription);
  await t
    .expect(classSelector.MessageValidateName.textContent)
    .eql("The Name field is required.");
});

test("Create a class with null description fields", async (t) => {
  await classFunc.CreateClass(randomName, "");
  await t
    .expect(classSelector.MessageValidateDescription.textContent)
    .eql("The Description field is required.");
});

test("Create a class with whitespace name field", async (t) => {
  await classFunc.CreateClass("                ", randomDescription);
  await t
    .expect(classSelector.MessageValidateName.textContent)
    .eql("The Name field is required.");
});

test("Create a class with whitespace description field", async (t) => {
  await classFunc.CreateClass(randomName, "          ");
  await t
    .expect(classSelector.MessageValidateDescription.textContent)
    .eql("The Description field is required.");
});

test("Create a valid class with space before name", async (t) => {
  await classFunc.CreateClass("          " + randomName, randomDescription);
  await classFunc.Search(randomDescription);
  var actualName = classSelector.NameValue.textContent;
  await t.expect(actualName).eql(randomName);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Create class success.");
  await classFunc.DeleteClass(randomDescription);
});

test("Create a valid class with space after name", async (t) => {
  await classFunc.CreateClass(randomName + "          ", randomDescription);
  await classFunc.Search(randomDescription);
  var actualName = classSelector.NameValue.textContent;
  await t.expect(actualName).eql(randomName);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Create class success.");
  await classFunc.DeleteClass(randomDescription);
});
test("Create a valid class with space before description", async (t) => {
  await classFunc.CreateClass(randomName, "          " + randomDescription);
  await classFunc.Search(randomDescription);
  var actualDescription = classSelector.DescriptionValue.textContent;
  await t.expect(actualDescription).eql(randomDescription);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Create class success.");
  await classFunc.DeleteClass(randomDescription);
});

test("Create a valid class with space after description", async (t) => {
  await classFunc.CreateClass(randomName, randomDescription + "          ");
  await classFunc.Search(randomDescription);
  var actualDescription = classSelector.DescriptionValue.textContent;
  await t.expect(actualDescription).eql(randomDescription);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Create class success.");
  await classFunc.DeleteClass(randomDescription);
});

test("Create a valid class", async (t) => {
  await classFunc.CreateClass(randomName, randomDescription);
  await classFunc.Search(randomDescription);
  var actualDescription = classSelector.DescriptionValue.textContent;
  await t.expect(actualDescription).eql(randomDescription);
  var actualName = classSelector.NameValue.textContent;
  await t.expect(actualName).eql(randomName);
  var actualToast = classSelector.ToastMessage.textContent;
  await t.expect(actualToast).eql("Create class success.");
  await classFunc.DeleteClass(randomDescription);
});

test("Create a duplicate class", async (t) => {
  await classFunc.CreateClass(randomName, randomDescription);
  await classFunc.CreateClass(randomName, randomDescription);
  var actualToast = classSelector.ToastMessage.textContent;
  await t
    .click(classSelector.ButtonCloseToast)
    .expect(actualToast)
    .eql("Duplicate class.");
  await classFunc.ReturnHome();
  await classFunc.DeleteClass(randomDescription);
});
