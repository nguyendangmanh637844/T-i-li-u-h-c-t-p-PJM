package bankInterface;

import account.IAccount;

public class ViettinBank implements IBank{
    private IAccount account;

    public ViettinBank(IAccount account) {
        this.account = account;
    }

    @Override
    public void createAccount() {
        System.out.println(account.getAccount());
    }
}
