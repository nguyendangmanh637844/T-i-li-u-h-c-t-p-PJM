import { t } from "testcafe";
import LoginSelector from "../Selector/LoginSelector";
const selector = new LoginSelector();
export default class LoginFunction {
  constructor() {}
  async Login(account, password) {
    if (!account && !password) {
        await t
        .typeText(selector.InputAccount, "account", {
          speed: 0.2,
        })
        .click(selector.InputAccount)
        .pressKey("ctrl+a delete")
        .typeText(selector.InputPassword, "password", {
          speed: 0.2,
        })
        .click(selector.InputPassword)
        .pressKey("ctrl+a delete")
        .click(selector.ButtonLogin);
    } else if (!account) {
        await t
        .typeText(selector.InputAccount, account, {
          speed: 0.2,
        })
        .typeText(selector.InputPassword, "password", {
          speed: 0.2,
        })
        .click(selector.InputPassword)
        .pressKey("ctrl+a delete")
        .click(selector.ButtonLogin);
    } else if (!password) {
      await tc
        .typeText(selector.InputAccount, "password")
        .click(selector.ButtonLogin);
    } else {
      await t
        .typeText(selector.InputAccount, account, {
          speed: 0.2,
        })
        .typeText(selector.InputPassword, password, {
          speed: 0.2,
        })
        .click(selector.ButtonLogin);
    }
  }
}
