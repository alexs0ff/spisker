interface Func<T1, TResult> {
    (item: T1): TResult;
}
interface Func2<T1,T2, TResult> {
    (item1: T1,item2:T2): TResult;
}

interface Func3<T1, T2,T3, TResult> {
    (item1: T1, item2: T2,item3 :T3): TResult;
}

export {
    Func,
    Func2,
    Func3
    }