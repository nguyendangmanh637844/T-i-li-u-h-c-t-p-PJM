import { Selector } from "testcafe";

export default class StudentSelector {
  constructor() {
    this.url = "https://localhost:5001/";
    this.ButtonAdd = Selector(".float-right.pb-3 > .btn.btn-success");
    this.ButtonDelete = Selector("tbody .btn-danger");
    this.ButtonEdit = Selector(
      ".table.table-bordered.table-striped .bi.bi-plus"
    );
    this.InputAddName = Selector(
      ".modal-body .form-group:nth-of-type(1) .form-control"
    );
    this.InputAddName = Selector(
      ".modal-body .form-group:nth-of-type(1) .form-control"
    );
    this.InputAddBirthday = Selector(
      ".modal-body .form-group:nth-of-type(2) .form-control"
    );
    this.ButtonAddSubmit = Selector(".modal-body .btn-primary");
    this.ButtonAddClose = Selector(".btn.btn-outline-primary");
    this.InputEditName = Selector(
      ".modal-body .form-group:nth-of-type(1) .form-control"
    );
    this.InputEditBirthday = Selector(
      ".modal-body .form-group:nth-of-type(2) .form-control"
    );
    this.ButtonEditSubmit = Selector(".modal-body button");
    this.ButtonEditClose = Selector(".btn.btn-primary.text-white");
    this.ButtonConfirmDelete = Selector(
      "#exampleModal > div > div > div.modal-footer > button.btn.btn-danger"
    );
    this.StudentTable = Selector(
      "#app > div > div.main > div.content.px-4 > div > div.card-body > div:nth-child(3) > table"
    );
    this.ToastMessage = Selector(
      "#app > div.blazored-toast-container.position-bottomright > div > div > p"
    );
    this.ButtonCloseToast = Selector(".blazored-toast-close-icon > svg");

  }
}
