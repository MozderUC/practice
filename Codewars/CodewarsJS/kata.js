// 7 kyi
// filterList https://www.codewars.com/kata/53dbd5315a3c69eed20002dd

// 6 kyi
// uniqueInOrder https://www.codewars.com/kata/54e6533c92449cc251001667
// iqTest https://www.codewars.com/kata/552c028c030765286c00007d
// arrayDiff https://www.codewars.com/kata/523f5d21c841566fde000009
// countSmileys https://www.codewars.com/kata/583203e6eb35d7980400002a

// 5 kyi
// phone https://www.codewars.com/kata/56baeae7022c16dd7400086e
// score https://www.codewars.com/kata/5270d0d18625160ada0000e4

// 4 kyi

// 3 kyi

// 2 kyi

// 1 kyi

exports.score = function (diceRolls) {
    var result = 0;
    var tripleMatchScore = {
        1: 1000,
        2: 200,
        3: 300,
        4: 400,
        5: 500,
        6: 600
    };
    var oneMatchScore = {
        1: 100,
        2: 0,
        3: 0,
        4: 0,
        5: 50,
        6: 0,
    };

    var groupedDiceRolls = {};
    diceRolls.forEach(d => { if (d in groupedDiceRolls) groupedDiceRolls[d]++; else groupedDiceRolls[d] = 1; });

    for (var group in groupedDiceRolls) {
        if (groupedDiceRolls[group] >= 3){
            result += tripleMatchScore[group];
            groupedDiceRolls[group] -= 3;
        }
        if (groupedDiceRolls[group] == 1){
            result += oneMatchScore[group];
            groupedDiceRolls[group] -= 1;
        }
    }

    return result;
}

exports.phone = function (phoneDirectory, phoneNumber) {
    var phoneDirectoryArr = phoneDirectory.split('\n');

    // google this options of regular expressions to understand this patterns: lookahead, lookbehind, non-greedy/greedy 
    var phoneNumberPattern = /(?<=\+)[\d-]+/g;
    var nameWithoutBracketsPattern = /(?<=\<).+?(?=\>)/g;
    var isTrashPattern = /(\s|^)([^A-Za-z0-9])+(\s|$)/g;

    // filter arr to select only elements with phone == phoneNumber
    var targetRecords = phoneDirectoryArr.filter(x => {
        var matchedPhones = x.match(phoneNumberPattern);
        if(matchedPhones == null || matchedPhones.length != 1)
            return false;
        
        if (matchedPhones[0] == phoneNumber)
            return true;
        
        return false;
    })

    // validation
    if (targetRecords.length > 1){
        return 'Error => Too many people: ' + phoneNumber;
    }

    if (targetRecords.length == 0){
        return 'Error => Not found: ' + phoneNumber;
    }

    var name = targetRecords[0].match(nameWithoutBracketsPattern);
    var address = targetRecords[0]
        .replace(nameWithoutBracketsPattern, '')
        .replace(phoneNumberPattern, '')
        .replace(/\s+|_/g, ' ')
        .split(' ')
        .map(x => {
            if(!x)
                return;
            if (x.match(isTrashPattern))
                return;
            if (x[0].match(/[^A-Za-z0-9]/))
                x = x.substring(1,x.length);
            if (x[x.length - 1] != '.' && x[x.length - 1].match(/[^A-Za-z0-9]/))
                x = x.substring(0, x.length - 1);
            return x;
        })
        .join(' ')
        .replace(/\s+/g, ' ')
        .trim();
        
    return "Phone => " + phoneNumber + ", Name => " + name + ", Address => " + address;
}

exports.countSmileys = function (arr) {
    return arr.filter(x => 
        (x[0] == ':' || x[0] == ';')
        && (x.length == 2 || (x.length == 3 && (x[1] == '-' || x[1] == '~')))
        && (x[x.length - 1] == ')' || x[x.length - 1] == 'D')).length;
}

exports.arrayDiff = function (a, b) {
    return [...a].filter(x => [...b].indexOf(x) == -1);
}

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