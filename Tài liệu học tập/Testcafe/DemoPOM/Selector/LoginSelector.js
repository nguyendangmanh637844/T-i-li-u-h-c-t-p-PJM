import { Selector } from "testcafe";

export default class LoginSelector {
  constructor() {
    this.url = "https://app-platonl-base-chn.azurewebsites.net/";
    this.InputAccount = Selector("input#accountName");
    this.InputPassword = Selector("input#password");
    this.ButtonLogin = Selector("button#btn-submit");
    this.MessageRequiredAccount = Selector(
      "[class='mb-2'] .validation-message"
    );
    this.MessageRequiredPassword = Selector(
      "[class='mb-3'] .validation-message"
    );
    this.ModalMessage = Selector(
      ".dxbs-modal-body.modal-body>i"
    );
    // this.=Selector("");
    // this.=Selector("");
  }
}
