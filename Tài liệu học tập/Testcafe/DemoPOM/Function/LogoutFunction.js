import LogoutSelector from "../Selector/LogoutSelector";
import { t } from "testcafe";
const selector = new LogoutSelector();
export default class LogoutFunction {
  constructor() {}
  async Logout() {
    await t.click(selector.ButtonUser).click(selector.ButtonLogout);
  }
}
