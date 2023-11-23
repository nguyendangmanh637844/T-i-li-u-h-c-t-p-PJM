package bankAbstract;

import account.IAccount;

public abstract class Bank {
    protected IAccount account;

    public Bank(IAccount account) {
        this.account = account;
    }

    public abstract void createAccount();
}
