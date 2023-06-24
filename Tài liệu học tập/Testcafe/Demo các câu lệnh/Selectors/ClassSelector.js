import { Selector } from "testcafe";

export default class ClassSelector {
  constructor() {
    this.url = "https://localhost:5001/";
    this.addButton = Selector(
      "#app > div > div.main > div.content.px-4 > div > div.card-header > div > a"
    );
    this.searchInput = Selector(
      "#app > div > div.main > div.content.px-4 > div > div.card-body > div > form > input"
    );
    this.searchButton = Selector(
      "#app > div > div.main > div.content.px-4 > div > div.card-body > div > form > button"
    );
    this.deleteButton = Selector(
      "tr:nth-of-type(1) > td:nth-of-type(4) > .btn.btn-danger"
    );
    this.editButton = Selector(
      "tr:nth-of-type(1) > td:nth-of-type(4) > .btn.btn-primary"
    );
    this.NameValue = Selector("tr:nth-of-type(1) > td:nth-of-type(2)");
    this.DescriptionValue = Selector("tr:nth-of-type(1) > td:nth-of-type(3)");
    this.ClassNameInput = Selector("input#className");
    this.DescriptionInput = Selector("input#description");
    this.SubmitAddClass = Selector("form#form > .btn.btn-success");
    this.InputUpdateClassName = Selector(
      "div:nth-of-type(2) > .form-control.rounded"
    );
    this.InputUpdateClassDescription = Selector("input#exampleInputEmail1");
    this.SaveUpdateButton = Selector(".btn.btn-success.float-right.mr-2");
    this.ConfirmDeleteButton = Selector(
      "#exampleModal > div > div > div.modal-footer > button.btn.btn-danger"
    );
    this.MessageValidateName = Selector("#form > div:nth-child(1) > div");
    this.MessageValidateDescription = Selector(
      "#form > div:nth-child(2) > div"
    );
    this.ToastMessage = Selector(
      "#app > div.blazored-toast-container.position-bottomright > div > div > p"
    );
    this.ButtonCloseToast = Selector(".blazored-toast-close-icon > svg");
    this.ButtonEditSave = Selector(".btn.btn-success.float-right.mr-2");
    this.ButtonEditDelete = Selector(".btn.btn-danger.mr-2");
    this.ButtonEditBack = Selector(".btn.btn-primary.float-right.mr-4");
    this.InputEditName = Selector("div:nth-of-type(2) > .form-control.rounded");
    this.InputEditDescription = Selector("input#exampleInputEmail1");
    this.TableClass = Selector('#app > div > div.main > div.content.px-4 > div > div.card-body > table');
    // this. = Selector('');
  }
}
