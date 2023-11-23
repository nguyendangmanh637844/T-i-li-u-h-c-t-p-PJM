package bankAbstract;

import account.IAccount;

public class VpBank extends Bank{
    public VpBank(IAccount account) {
        super(account);
    }

    @Override
    public void createAccount() {
        System.out.println(account);
    }
}
