import { Selector } from "testcafe";
import PractisePage from "../Selectors/PractiseSelector";
const page = new PractisePage();
fixture`Practise`.page("https://rahulshettyacademy.com/AutomationPractice/");

test("radio", async (t) => {
  await t.click(page.RatioButton3);
  await t.expect(page.RatioButton3.checked).eql(true);
});
test("suggess class", async (t) => {
  await t.typeText(page.InputSuggession, "vi").wait(2000);
  const display = await page.Dropdown.getStyleProperty("display");
  await t.expect(display).notEql("none");
  await t.click(page.ItemDropdown);
  const content = page.InputSuggession.value;
  await t.expect(content).eql("Bolivia");
});
test("dropdown", async (t) => {
  await t.click(page.DropdownExam).click(page.ItemDropdownExam);
  const isChecked = page.ItemDropdownExam.value;
  await t.expect(isChecked).eql("option1");
});
test("checkbox", async (t) => {
  await t.click(page.CheckBox1);
  await t.expect(page.CheckBox1.checked).eql(true);
});
test("switch brownser", async (t) => {
  const FirstWindow = await t.getCurrentWindow();
  const SecondWindow = await t
    .click(page.ButtonOpenWindow)
    .maximizeWindow()
    .getCurrentWindow();
  const ThirdWindow = await t.switchToPreviousWindow().getCurrentWindow();
  await t.expect(SecondWindow).notEql(FirstWindow);
  await t.expect(ThirdWindow).eql(FirstWindow);
});
test("tab", async (t) => {
  await t
    .expect(
      Selector(page.ButtonOpenTab).withAttribute("target", "_blank").exists
    )
    .ok();
  await t
    .expect(Selector(page.ButtonOpenTab).getAttribute("href"))
    .eql("https://www.rahulshettyacademy.com/");
});
test("alert dialog", async (t) => {
  await t
    .typeText(page.InputAlert, "dummies text")
    .setNativeDialogHandler(() => true)
    .click(page.ButtonAlert);
  const text = await t.getNativeDialogHistory();
  const a = text.at(0).text;
  await t.expect(a).contains("dummies text");
});
test("table", async (t) => {
  let table = page.Table;
  let rowCount = await table.find("tr").count;
  let TextInstructor = await table.find("tr").nth(1).find("td").nth(0)
    .textContent;
  let TextCourse = await table.find("tr").nth(1).find("td").nth(1).textContent;
  let TextPrice = await table.find("tr").nth(1).find("td").nth(2).textContent;
  await t
    .expect(TextInstructor)
    .eql("Rahul Shetty")
    .expect(TextCourse)
    .eql("Selenium Webdriver with Java Basics + Advanced + Interview Guide")
    .expect(TextPrice)
    .eql("30");

  TextInstructor = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(0).textContent;
  TextCourse = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(1).textContent;
  TextPrice = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(2).textContent;
  await t
    .expect(TextInstructor)
    .eql("Rahul Shetty")
    .expect(TextCourse)
    .eql("WebServices / REST API Testing with SoapUI")
    .expect(TextPrice)
    .eql("35");
});
test("Get attribute", async (t) => {
  await t.click(page.ButtonHide);
  var isHide = page.InputDisplay.getStyleProperty("display");
  await t.expect(isHide).eql("none");
  await t.click(page.ButtonShow);
  var isShow = page.InputDisplay.getAttribute("style");
  await t.expect(isShow).eql("display: block;");
});
test("Scroll", async (t) => {
  await t.scroll(page.TableFix, "bottom");
  let table = page.TableFix;
  let rowCount = await table.find("tr").count;
  let TextName = await table.find("tr").nth(1).find("td").nth(0).textContent;
  let TextPosition = await table.find("tr").nth(1).find("td").nth(1)
    .textContent;
  let TextCity = await table.find("tr").nth(1).find("td").nth(2).textContent;
  let TextAmount = await table.find("tr").nth(1).find("td").nth(3).textContent;
  await t
    .expect(TextName)
    .eql("Alex")
    .expect(TextPosition)
    .eql("Engineer")
    .expect(TextCity)
    .eql("Chennai")
    .expect(TextAmount)
    .eql("28");

  TextName = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(0).textContent;
  TextPosition = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(1).textContent;
  TextCity = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(2).textContent;
  TextAmount = await table
    .find("tr")
    .nth(Math.round(rowCount / 2))
    .find("td")
    .nth(3).textContent;
  await t
    .expect(TextName)
    .eql("Jack")
    .expect(TextPosition)
    .eql("Engineer")
    .expect(TextCity)
    .eql("Pune")
    .expect(TextAmount)
    .eql("32");
});
test("hover", async (t) => {
  await t.hover(page.ButtonHover);
  let isShow = page.PopUpHover.getStyleProperty("display");
  await t.expect(isShow).notEql("none");
});
test("iframe", async (t) => {
  let mainWindow = await t.getCurrentWindow();
  await t.switchToIframe(page.Iframe);
  let Window = await t.switchToMainWindow().getCurrentWindow();
  await t.expect(Window).eql(mainWindow);
});
