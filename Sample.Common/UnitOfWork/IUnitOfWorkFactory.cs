namespace Sample.Common.UnitOfWork {
    public interface IUnitOfWorkFactory {
        IUnitOfWork CreateUnitOfWork();
    }
}
