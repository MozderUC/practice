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
// calculatingWithFunctions https://www.codewars.com/kata/525f3eda17c7cd9f9e000b39
// add https://www.codewars.com/kata/539a0e4d85e3425cb0000a88

// 4 kyi
// mix https://www.codewars.com/kata/5629db57620258aa9d000014
// sumIntervals https://www.codewars.com/kata/52b7ed099cdc285c300001cd
// stripComments https://www.codewars.com/kata/51c8e37cee245da6b40000bd
// topThreeWords https://www.codewars.com/kata/51e056fe544cf36c410000fb

// 3 kyi
// todo checkRange https://www.codewars.com/kata/591e833267cd75cb02000007

// 2 kyi

// 1 kyi

exports.topThreeWords = function (text){
    var group = {};
    text.split(' ').forEach(w => {
        var x = w.replace(/[^A-Za-z']|/g, '').toLowerCase().trim();
        if (!x || x === "'") return;

        if (x in group) group[x]++;
        else group[x]=0;
    })
    
    return Object.keys(group).sort(function(a,b){return group[b]-group[a]}).slice(0,3);
}

//exports.checkRange = (a,x,y)=>{var c = 0;a.forEach(i=>{if(i>=x&&i<=y)c++;});return c;}
//exports.checkRange=(a,x,y)=>a.filter(k=>k>=x&&k<=y).length
//exports.checkRange=(a,x,y)=>a.filter(k=>k>=x&k<=y).length

exports.checkRange=(a,x,y)=>{

}

exports.stripComments = function (input, markers){
    var arrInput = input.split('\n');
    var result = '';

    arrInput.forEach((x) => {
        var index = [...x].findIndex(c => markers.includes(c));

        if (index == -1)
        result += x.trim() + '\n';
        else
        result += x.substring(0, index).trim() + '\n';
    })

    return result.substring(0, result.length - 1);
}

exports.sumIntervals = function (intervalsArr){
    var intervalEnumerable = {};

    for (let i = 0; i < intervalsArr.length; i++) {
        for (let index = intervalsArr[i][0]; index < intervalsArr[i][1]; index++) {
            if (!(index in intervalEnumerable)) intervalEnumerable[index] = 1;
        }        
    }

    return Object.keys(intervalEnumerable).length;
}

// inspired of https://stackoverflow.com/questions/26656718/how-to-achieve-arbitrary-chain-on-function-call-in-javascript
exports.add = function (n) {
    var f = function(b){
        return exports.add(n+b);
    };
    f.valueOf = function(){
        return n;
    };
    return f;
}

// calculatingWithFunctions start
exports.zero = (arg) => processDigit(0, arg)
exports.one = (arg) =>  processDigit(1, arg)
exports.two = (arg) => processDigit(2, arg)
exports.three = (arg) => processDigit(3, arg)
exports.four = (arg) => processDigit(4, arg)
exports.five = (arg) => processDigit(5, arg)
exports.six = (arg) => processDigit(6, arg)
exports.seven = (arg) => processDigit(7, arg)
exports.eight = (arg) => processDigit(8, arg)
exports.nine = (arg) => processDigit(9, arg)

exports.plus = (arg) => '+' + arg;
exports.minus = (arg) => '-' + arg;
exports.times = (arg) => '*' + arg;
exports.dividedBy = (arg) => '/' + arg;

function processDigit(digit, arg){
    if (!arg || arg.charAt(0).match(/\d/)){
        return digit;
    } else{
        return calculate(digit, arg.charAt(0), parseInt(arg.charAt(1)));
    } 
}

function calculate(firstDigit, operator, secondDigit){
    switch (operator) {
        case '+':
          return firstDigit + secondDigit;
        case '-':
          return firstDigit - secondDigit;
        case '*':
          return firstDigit * secondDigit;
        case '/':
          return parseInt(firstDigit / secondDigit);
      }
}
// calculatingWithFunctions end

exports.mix = function (s1, s2) {

    var alphabet = ["a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"];

    var resultArr = [];
    var result = '';

    var groupedS1 = {};
    var groupedS2 = {};

    [...s1].forEach(c => {
        if (c.match(/[^a-z]/)) return;

        if (c in groupedS1) groupedS1[c] += c;

        else groupedS1[c] = c; });

    [...s2].forEach(c => {
        if (c.match(/[^a-z]/)) return;

        if (c in groupedS2) groupedS2[c] += c;

        else groupedS2[c] = c; });


    alphabet.forEach(a => {
        if (groupedS1[a] && !groupedS2[a] && groupedS1[a].length > 1)
            resultArr.push(`1:${groupedS1[a]}`);

        if (!groupedS1[a] && groupedS2[a] && groupedS2[a].length > 1)
            resultArr.push(`2:${groupedS2[a]}`);

        if (groupedS1[a] && groupedS2[a] && (groupedS1[a].length > 1 || groupedS2[a].length > 1)) {
            if (groupedS1[a].length > groupedS2[a].length)
                resultArr.push(`1:${groupedS1[a]}`);
            else if (groupedS1[a].length < groupedS2[a].length)
                resultArr.push(`2:${groupedS2[a]}`);
            else if (groupedS1[a].length == groupedS2[a].length)
                resultArr.push(`=:${groupedS2[a]}`);
        }
    })

    resultArr.sort((a,b) => {
        if (b.length > a.length)
            return 1;
        else if (b.length < a.length) 
            return -1;
    
        if (b > a)
            return -1;
        else
            return 1
    })

    resultArr.forEach(a => {
        result += a + '/';
    })
    
    return result.slice(0, -1);
}

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