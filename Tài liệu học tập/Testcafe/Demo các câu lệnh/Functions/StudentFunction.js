import ClassSelector from "../Selectors/ClassSelector";
import StudentSelector from "../Selectors/StudentSelector";
import { t } from "testcafe";
import ClassFunction from "./ClassFunction";
const studentSelectors = new StudentSelector();
const classSelectors = new ClassSelector();
const classFunc = new ClassFunction();

var randomName = "TestAuto_" + Math.floor(Math.random() * 99 + 1);
var randomDescription = classFunc.GetRandomString(10);

export default class StudentFunction {
  contructor() {
    this.randomName = randomName;
    this.randomDescription = randomDescription;
  }
  async EditClass() {
    await classFunc.CreateClass(randomName, randomDescription);
    await classFunc.Search(randomDescription);
    await t.click(classSelectors.editButton);
  }
  async DeleteClass() {
    await classFunc.DeleteClass(randomDescription);
  }
  async DeleteStudent() {
    await t
      .click(studentSelectors.ButtonDelete)
      .click(studentSelectors.ButtonConfirmDelete);
  }
  async ReturnHome() {
    await classFunc.ReturnHome();
  }
  async AddStudent(name, birthday) {
    if (!name && !birthday) await t.click(studentSelectors.ButtonAdd).click(studentSelectors.ButtonAddSubmit);
    else if (!name) await t.click(studentSelectors.ButtonAdd).click(studentSelectors.ButtonAddSubmit);
    else if (!birthday) {
      await t
        .click(studentSelectors.ButtonAdd)
        .typeText(studentSelectors.InputAddName, name)
        .click(studentSelectors.ButtonAddSubmit);
    } else {
      await t
        .click(studentSelectors.ButtonAdd)
        .typeText(studentSelectors.InputAddName, name)
        .typeText(studentSelectors.InputAddBirthday, birthday)
        .click(studentSelectors.ButtonAddSubmit);
    }
  }
  async UpdateStudent(updateName, updateBirthday){
    await t.click(studentSelectors.ButtonEdit)
    .click(studentSelectors.InputEditName)
    .pressKey('ctrl+a delete')
    .typeText(studentSelectors.InputEditName, updateName)
    .typeText(studentSelectors.InputEditBirthday, updateBirthday)
    .click(studentSelectors.ButtonEditSubmit);
  }
  getRandomDate(start, end) {
    var d = new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime())),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
}
