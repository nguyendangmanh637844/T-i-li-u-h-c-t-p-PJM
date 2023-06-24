import { Selector } from "testcafe";

export default class LogoutSelector{
    constructor(){
        this.ButtonUser = Selector("i[title='User Info']");
        this.ButtonLogout = Selector("li:nth-of-type(3) .dropdown-item");
    }
}