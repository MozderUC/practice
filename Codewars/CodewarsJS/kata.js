// 7 kyi
// filterList https://www.codewars.com/kata/53dbd5315a3c69eed20002dd

// 6 kyi
// uniqueInOrder https://www.codewars.com/kata/54e6533c92449cc251001667

// 5 kyi

// 4 kyi

// 3 kyi

// 2 kyi

// 1 kyi

exports.filterList = function (l) {
    return l.filter(c => typeof c != "string");
}

exports.uniqueInOrder = function(iterable) {
    var result = [];
    for (var i = 0; i < iterable.length; i++){
        if (i > 0 && iterable[i] == iterable[i-1])
            continue;
        result.push(iterable[i]);
    }

    return result;
}