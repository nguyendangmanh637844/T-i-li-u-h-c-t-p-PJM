import { ClientFunction } from "testcafe";
import AppSetting from "../appsetting";
import LoginFunction from "../Function/LoginFunction";
import LoginSelector from "../Selector/LoginSelector";

const func = new LoginFunction();
const selector = new LoginSelector();
const appsetting = new AppSetting();

fixture`Login`.page(selector.url);

test("Valid Login", async (t) => {
  await func.Login(appsetting.account, appsetting.password);
  const url = await ClientFunction(() => window.location.href)();
  await t.expect(url).notEql(selector.url);
});

test("Empty all fields", async (t) => {
  await func.Login("", "");
  await t
    .expect(selector.MessageRequiredAccount.textContent)
    .eql("Der Kontoname ist erforderlich")
    .expect(selector.MessageRequiredPassword.textContent)
    .eql("Passwort wird benötigt");
});
test("Empty account", async (t) => {
  await func.Login("", appsetting.password);
  await t
    .expect(selector.MessageRequiredAccount.textContent)
    .eql("Der Kontoname ist erforderlich");
});
test("Empty password", async (t) => {
  await func.Login(appsetting.account, "");
  await t
    .expect(selector.MessageRequiredPassword.textContent)
    .eql("Passwort wird benötigt");
});
test("Unvalid login", async (t) => {
  await func.Login("testAcount%1*#&3$^", "testPassword324^&*");
  await t
    .expect(selector.ModalMessage.textContent)
    .eql("Falscher Kontoname oder Passwort.");
});
