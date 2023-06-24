import { t } from "testcafe";
import ClassSelector from "../Selectors/ClassSelector";
const selectors = new ClassSelector();
export default class ClassFunction {
  constructor() {}
  async CreateClass(className, description) {
    await t.click(selectors.addButton);
    if (!className && !description) await t.click(selectors.SubmitAddClass);
    else if (!className) {
      await t
        .typeText(selectors.DescriptionInput, description)
        .click(selectors.SubmitAddClass);
    } else if (!description) {
      await t
        .typeText(selectors.ClassNameInput, className)
        .click(selectors.SubmitAddClass);
    } else {
      await t
        .typeText(selectors.DescriptionInput, description)
        .typeText(selectors.ClassNameInput, className)
        .click(selectors.SubmitAddClass);
    }
  }
  async Search(searchString) {
    await t
      .click(selectors.searchInput)
      .pressKey("ctrl+a delete")
      .typeText(selectors.searchInput, searchString)
      .click(selectors.searchButton);
  }
  async UpdateClass(stringToSearch, newClassName, newDescription) {
    await this.Search(stringToSearch);
    await t.click(selectors.editButton);
    if(!newClassName && !newDescription){
      await t
        .click(selectors.InputUpdateClassName)
        .pressKey("ctrl+a delete")
        .click(selectors.InputUpdateClassDescription)
        .pressKey("ctrl+a delete")
        .click(selectors.ButtonEditSave);
    }
    else if (!newClassName) {
      await t
        .click(selectors.InputUpdateClassDescription)
        .pressKey("ctrl+a delete")
        .typeText(selectors.InputUpdateClassDescription, newDescription)
        .click(selectors.ButtonEditSave);
    } else if (!newDescription) {
      await t
        .click(selectors.InputUpdateClassName)
        .pressKey("ctrl+a delete")
        .typeText(selectors.InputUpdateClassName, newClassName)
        .click(selectors.ButtonEditSave);
    } else {
      await t
        .click(selectors.InputUpdateClassName)
        .pressKey("ctrl+a delete")
        .typeText(selectors.InputUpdateClassName, newClassName)
        .click(selectors.InputUpdateClassDescription)
        .pressKey("ctrl+a delete")
        .typeText(selectors.InputUpdateClassDescription, newDescription)
        .click(selectors.ButtonEditSave);
    }
  }

  async DeleteClass(stringToSearch) {
    await this.Search(stringToSearch);
    await t.click(selectors.deleteButton).click(selectors.ConfirmDeleteButton);
  }

  async ReturnHome() {
    await t.navigateTo(selectors.url);
  }

  GetRandomString(length) {
    var result = "";
    var characters =
      "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
  }
}
