import account.PhoneAccount;
import account.VipAccount;
import bankAbstract.Bank;
import bankAbstract.Pvcombank;
import bankInterface.IBank;
import bankInterface.ViettinBank;

public class Main {
    public static void main(String[] args) {
        Bank pvcombank = new Pvcombank(new VipAccount());
        pvcombank.createAccount();
        IBank viettin = new ViettinBank(new PhoneAccount());
        viettin.createAccount();
    }
}