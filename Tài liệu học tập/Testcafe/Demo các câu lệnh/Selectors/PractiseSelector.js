import { Selector } from "testcafe";

export default class PractisePage {
  constructor() {
    this.RatioButton1 = Selector("[for='radio1'] .radioButton");
    this.RatioButton2 = Selector("[for='radio2'] .radioButton");
    this.RatioButton3 = Selector("[for='radio3'] .radioButton");
    this.InputSuggession = Selector("#autocomplete");
    this.Dropdown = Selector(".ui-widget-content");
    this.ItemDropdown = Selector("li:nth-of-type(1) > .ui-menu-item-wrapper");
    this.DropdownExam = Selector("select#dropdown-class-example");
    this.ItemDropdownExam = Selector("[name] [value='option1']");
    this.CheckBox1 = Selector("#checkBoxOption1");
    this.ButtonOpenWindow = Selector("button#openwindow");
    this.ButtonOpenTab = Selector("a#opentab");
    this.InputAlert = Selector("#name");
    this.ButtonAlert = Selector("input#alertbtn");
    this.Table = Selector("[border]");
    this.InputDisplay = Selector("input#displayed-text");
    this.ButtonHide = Selector("input#hide-textbox");
    this.ButtonShow = Selector("input#show-textbox");
    this.TableFix = Selector(".tableFixHead #product");
    this.ButtonHover = Selector("button#mousehover");
    this.PopUpHover = Selector(".mouse-hover-content");
    this.Iframe = Selector("#courses-iframe");
  }
}
