import ClassFunction from "../../Functions/ClassFunction";
import StudentFunction from "../../Functions/StudentFunction";
import ClassSelector from "../../Selectors/ClassSelector";
import StudentSelector from "../../Selectors/StudentSelector";
const studentSelector = new StudentSelector();
const studentFunc = new StudentFunction();
const classSelector = new ClassSelector();
const classFunc = new ClassFunction();
var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);
var randomDate = studentFunc.getRandomDate(new Date(2012, 0 , 1), new Date());
fixture`Create student`
  .page(studentSelector.url)
  .beforeEach(async (t) => {
    await studentFunc.EditClass();
    await studentFunc.AddStudent(randomName, "");
  })
  .afterEach(async (t) => {
    await studentFunc.ReturnHome();
    await studentFunc.DeleteClass();
  });

test("Not change", async (t) => {
  await t
    .click(studentSelector.ButtonCloseToast)
    .wait(3000)
    .click(studentSelector.ButtonEdit)
    .click(studentSelector.ButtonEditSubmit);
  var toastMessage = studentSelector.ToastMessage.textContent;
  await t.expect(toastMessage).eql("Not thing change");
});

test("Check null fields", async (t) => {
  await t.click(studentSelector.ButtonEdit);
  var isRequired = studentSelector.InputEditName.hasAttribute("required");
  await t.expect(isRequired).eql(true);
  var isRequired = studentSelector.InputEditBirthday.hasAttribute("required");
  await t.expect(isRequired).eql(true);
});

test("Update with duplicate name", async (t) => {
await t.wait(5000);
await studentFunc.AddStudent(randomName+'_1',randomDate);
await t.click(studentSelector.ButtonEdit);
await studentFunc.UpdateStudent(randomName, randomName);
var message = studentSelector.ToastMessage.textContent;
await t.expect(message).eql('Duplicate student');

});

test("Valid updation a student", async (t) => {
    await studentFunc.UpdateStudent(randomName+'_1', randomDate);
    var actualName = studentSelector.StudentTable.find('tr').nth(1).find('td').nth(0).textContent;
    await t.expect(actualName).eql(randomName+'_1');
});

test("Valid updation a student with space before", async (t) => {
  await studentFunc.UpdateStudent("                    "+randomName+'_1', randomDate);
  var actualName = studentSelector.StudentTable.find('tr').nth(1).find('td').nth(0).textContent;
  await t.expect(actualName).eql(randomName+'_1');
});

test("Valid updation a student with space after", async (t) => {
  await studentFunc.UpdateStudent(randomName+'_1                  ', randomDate);
  var actualName = studentSelector.StudentTable.find('tr').nth(1).find('td').nth(0).textContent;
  await t.expect(actualName).eql(randomName+'_1');
});

