interface Action<T> {
    (item: T): void;
}

interface Action<T> {
    (item: T): void;
}


interface Action2<T1,T2> {
    (item1: T1,item2:T2): void;
}

interface Action3<T1, T2, T3> {
    (item1: T1, item2: T2,item3:T3): void;
}


export {
    Action,
    Action2,
    Action3
    }