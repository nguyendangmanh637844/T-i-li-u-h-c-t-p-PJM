import StudentFunction from "../../Functions/StudentFunction";
import StudentSelector from "../../Selectors/StudentSelector";
const studentSelector = new StudentSelector();
const studentFunc = new StudentFunction();
var randomName =  "TestAuto_" + Math.floor(Math.random() * 99 + 1);
fixture`Delete student`.page(studentSelector.url).beforeEach(async (t) => {
  await studentFunc.EditClass();
});

test("Delete student", async (t) => {
  await studentFunc.AddStudent(
    randomName,
    Date.now.toString("yyyy-MM-dd")
  );
  await studentFunc.DeleteStudent();
  var countRow = await studentSelector.StudentTable.find('tr').count;
  await t.expect(countRow).eql(1);
});
