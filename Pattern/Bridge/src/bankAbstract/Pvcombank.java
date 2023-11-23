package bankAbstract;

import account.IAccount;

public class Pvcombank extends Bank {
    public Pvcombank(IAccount account){
        super(account);
    }
    @Override
    public void createAccount() {
        System.out.println(account.getAccount());
    }
}
