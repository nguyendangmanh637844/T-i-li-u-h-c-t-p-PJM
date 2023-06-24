import StudentFunction from "../../Functions/StudentFunction";
import StudentSelector from "../../Selectors/StudentSelector";
const studentSelector = new StudentSelector();
const studentFunc = new StudentFunction();
var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);

fixture`Create student`
  .page(studentSelector.url)
  .beforeEach(async (t) => {
    await studentFunc.EditClass();
  })
  .afterEach(async (t) => {
    await studentFunc.ReturnHome();
    await studentFunc.DeleteClass();
  });

test("Check required", async (t) => {
  await t.click(studentSelector.ButtonAdd);
  var isRequired = studentSelector.InputAddName.hasAttribute("required");
  await t.expect(isRequired).eql(true);
  var isRequired = studentSelector.InputAddBirthday.hasAttribute("required");
  await t.expect(isRequired).eql(true);
});

test("Add a valid student", async (t) => {
  var beforeRowsCount = await studentSelector.StudentTable.find("tr").count;
  await studentFunc.AddStudent(
    studentFunc.randomName,
    Date.now.toString("yyyy-MM-dd")
  );
  var afterRowsCount = await studentSelector.StudentTable.find("tr").count;
  await t.expect(afterRowsCount).gt(beforeRowsCount);
});

test("Add a valid student with white space before name", async (t) => {
  var beforeRowsCount = await studentSelector.StudentTable.find("tr").count;
  await studentFunc.AddStudent(
    "               " + studentFunc.randomName,
    Date.now.toString("yyyy-MM-dd")
  );
  var afterRowsCount = await studentSelector.StudentTable.find("tr").count;
  await t.expect(afterRowsCount).gt(beforeRowsCount);
});

test("Add a valid student with white space after name", async (t) => {
  var beforeRowsCount = await studentSelector.StudentTable.find("tr").count;
  await studentFunc.AddStudent(
    studentFunc.randomName + "               ",
    Date.now.toString("yyyy-MM-dd")
  );
  var afterRowsCount = await studentSelector.StudentTable.find("tr").count;
  await t.expect(afterRowsCount).gt(beforeRowsCount);
});

test("Add a duplicate student", async (t) => {
  await studentFunc.AddStudent(randomName, Date.now.toString("yyyy-MM-dd"));
  await t.wait(5000);
  await studentFunc.AddStudent(randomName, Date.now.toString("yyyy-MM-dd"));
  var toastMessage = await studentSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Duplicate student");
});
