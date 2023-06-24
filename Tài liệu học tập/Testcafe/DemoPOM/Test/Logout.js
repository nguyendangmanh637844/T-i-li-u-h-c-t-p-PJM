import { ClientFunction } from "testcafe";
import AppSetting from "../appsetting";
import LoginFunction from "../Function/LoginFunction";
import LogoutFunction from "../Function/LogoutFunction";
import LoginSelector from "../Selector/LoginSelector";

const func = new LoginFunction();
const selector = new LoginSelector();
const appsetting = new AppSetting();
const logout = new LogoutFunction();
fixture`Logout`.page(selector.url)
.beforeEach(async () =>{
    await func.Login(appsetting.account, appsetting.password);
})
test("Logout", async (t) => {
    await logout.Logout();
    var url = await ClientFunction(() => window.location.href)();
    await t.expect(url).eql("https://app-platonl-base-chn.azurewebsites.net/login?returnUrl=https%3a%2f%2fapp-platonl-base-chn.azurewebsites.net%2f"); 
});
