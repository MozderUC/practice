// 7 kyi
// filterList https://www.codewars.com/kata/53dbd5315a3c69eed20002dd

// 6 kyi
// uniqueInOrder https://www.codewars.com/kata/54e6533c92449cc251001667

// 5 kyi

// 4 kyi

// 3 kyi

// 2 kyi

// 1 kyi

exports.iqTest = function (numbers) {

    if (typeof numbers != "string")
        return -1

    var arr = numbers.split(' ');
    var findEven = [arr[0], arr[1], arr[2]].filter(x => x % 2 == 0).length <= 1;

    return arr.findIndex((element) => {
        if (findEven && element % 2 == 0) {
            return element;
        } else if (!findEven && element % 2 != 0) {
            return element;
        }

        return false;
    }) + 1;
}

exports.filterList = function (l) {
    return l.filter(c => typeof c != "string");
}

exports.uniqueInOrder = function (iterable) {
    var result = [];
    for (var i = 0; i < iterable.length; i++) {
        if (i > 0 && iterable[i] == iterable[i - 1])
            continue;
        result.push(iterable[i]);
    }

    return result;
}

exports.test = function (t) {
    return t;
}