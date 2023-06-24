import ClassFunction from "../../Functions/ClassFunction";
import ClassSelector from "../../Selectors/ClassSelector";

const classSelector = new ClassSelector();
const classFunc = new ClassFunction();

var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);
var randomDescription = classFunc.GetRandomString(10);

fixture`Delete Class`.page(classSelector.url).beforeEach(async (t) => {
  await classFunc.CreateClass(randomName, randomDescription);
});

test("Delete outside", async (t) => {
  await classFunc.DeleteClass(randomName);
  await classFunc.ReturnHome();
  await classFunc.Search(randomDescription);
  var rowCount = classSelector.TableClass.find("tr").count;
  await t.expect(rowCount).eql(1);
});

test("Delete inside", async (t) => {
  await t
    .click(classSelector.editButton)
    .click(classSelector.ButtonEditDelete)
    .click(classSelector.ConfirmDeleteButton);
  await classFunc.ReturnHome();
  await classFunc.Search(randomDescription);
  var rowCount = classSelector.TableClass.find("tr").count;
  await t.expect(rowCount).eql(1);
});
