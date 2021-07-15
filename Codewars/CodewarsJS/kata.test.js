var kata = require('./kata.js');


test('stripComments_stringWithCommentsMarkers_stringWithoutComments', () => {
    expect(kata.stripComments("a #b\nc\nd $e f g", [ '#', '$' ])).toEqual("a\nc\n\d");
    expect(kata.stripComments("apples, plums % and bananas\npears\noranges !applesauce", ["%", "!"])).toEqual("apples, plums\npears\noranges");
    expect(kata.stripComments("Q @b\nu\ne -e f g", ["@", "-"])).toEqual("Q\nu\ne");
});

test('sumIntervals_arrayOfIntervals_sumOfThem', () => {

    expect(kata.sumIntervals([[1,5]])).toBe(4);
    expect(kata.sumIntervals([[1,5],[6,10]])).toBe(8);
    expect(kata.sumIntervals([[1,5]])).toBe(4);
    expect(kata.sumIntervals([[1,4],[7, 10],[3, 5]])).toBe(7);
});

test('add_chainAddingFunction_sumResult', () => {
    expect(kata.add(5)(5)(5).valueOf()).toEqual(15);
});

test('calculatingWithFunctions_aSequenceOfThreeFunctionCalls_resultOfArithmeticExpression', () => {
    expect(kata.nine(kata.plus(kata.one()))).toBe(10);
    expect(kata.nine(kata.dividedBy(kata.two()))).toBe(4);
    expect(kata.three(kata.times(kata.two()))).toBe(6);
});

test('mix_twoStrings_diffOfTheStrings', () => {
    expect(kata.mix("Are they here", "yes, they are here")).toEqual("2:eeeee/2:yy/=:hh/=:rr");
    expect(kata.mix("looping is fun but dangerous", "less dangerous than coding")).toEqual("1:ooo/1:uuu/2:sss/=:nnn/1:ii/2:aa/2:dd/2:ee/=:gg");
    expect(kata.mix(" In many languages", " there's a pair of functions")).toEqual("1:aaa/1:nnn/1:gg/2:ee/2:ff/2:ii/2:oo/2:rr/2:ss/2:tt");
    expect(kata.mix("Lords of the Fallen", "gamekult")).toEqual("1:ee/1:ll/1:oo");
    expect(kata.mix("codewars", "codewars")).toEqual("");
    expect(kata.mix("A generation must confront the looming ", "codewarrs")).toEqual("1:nnnnn/1:ooooo/1:tttt/1:eee/1:gg/1:ii/1:mm/=:rr");
});

test('score_arrayOf5DiceRollResults_TotalScoreCalculatingByKataRules', () => {
    expect(kata.score([1, 1, 1, 1, 3])).toEqual(1100);
    expect(kata.score([2, 3, 4, 6, 2])).toEqual(0);
    expect(kata.score([4, 4, 4, 3, 3])).toEqual(400);
    expect(kata.score([2, 4, 4, 5, 4])).toEqual(450);
});

const phoneDirectory = "/+1-541-754-3010 156 Alphand_St. <J Steeve>\n 133, Green, Rd. <E Kustur> NY-56423 ;+1-541-914-3010\n"
+ "+1-541-984-3012 <P Reed> /PO Box 530; Pollocksville, NC-28573\n :+1-321-512-2222 <Paul Dive> Sequoia Alley PQ-67209\n"
+ "+1-741-984-3090 <Peter Reedgrave> _Chicago\n :+1-921-333-2222 <Anna Stevens> Haramburu_Street AA-67209\n"
+ "+1-111-544-8973 <Peter Pan> LA\n +1-921-512-2222 <Wilfrid Stevens> Wild Street AA-67209\n"
+ "<Peter Gone> LA ?+1-121-544-8974 \n <R Steell> Quora Street AB-47209 +1-481-512-2222\n"
+ "<Arthur Clarke> San Antonio $+1-121-504-8974 TT-45120\n <Ray Chandler> Teliman Pk. !+1-681-512-2222! AB-47209,\n"
+ "<Sophia Loren> +1-421-674-8974 Bern TP-46017\n <Peter O'Brien> High Street +1-908-512-2222; CC-47209\n"
+ "<Anastasia> +48-421-674-8974 Via Quirinal Roma\n <P Salinger> Main Street, +1-098-512-2222, Denver\n"
+ "<C Powel> *+19-421-674-8974 Chateau des Fosses Strasbourg F-68000\n <Bernard Deltheil> +1-498-512-2222; Mount Av.  Eldorado\n"
+ "+1-099-500-8000 <Peter Crush> Labrador Bd.\n +1-931-512-4855 <William Saurin> Bison Street CQ-23071\n"
+ "<P Salinge> Main Street, +1-098-512-2222, Denve\n"

test('phone_phoneDirectoryAndOnePhoneNumber_formattedRecordForThatPhoneNumber', () => {
    expect(kata.phone(phoneDirectory, '1-541-754-3010')).toEqual("Phone => 1-541-754-3010, Name => J Steeve, Address => 156 Alphand St.");
    expect(kata.phone(phoneDirectory, '1-908-512-2222')).toEqual("Phone => 1-908-512-2222, Name => Peter O'Brien, Address => High Street CC-47209");
    expect(kata.phone(phoneDirectory, '48-421-674-8974')).toEqual("Phone => 48-421-674-8974, Name => Anastasia, Address => Via Quirinal Roma");
    expect(kata.phone(phoneDirectory, '1-921-512-2222')).toEqual("Phone => 1-921-512-2222, Name => Wilfrid Stevens, Address => Wild Street AA-67209");
    expect(kata.phone(phoneDirectory, '1-121-504-8974')).toEqual("Phone => 1-121-504-8974, Name => Arthur Clarke, Address => San Antonio TT-45120");
    expect(kata.phone(phoneDirectory, '1-498-512-2222')).toEqual("Phone => 1-498-512-2222, Name => Bernard Deltheil, Address => Mount Av. Eldorado");
    expect(kata.phone(phoneDirectory, '1-098-512-2222')).toEqual("Error => Too many people: 1-098-512-2222");
    expect(kata.phone(phoneDirectory, '5-555-555-5555')).toEqual("Error => Not found: 5-555-555-5555");
});

test('countSmileys_arrayWithSmiles_countOfHappySmiles', () => {
    expect(kata.countSmileys([';~~D'])).toBe(0);
    expect(kata.countSmileys([])).toBe(0);
    expect(kata.countSmileys([':D',':~)',';~D',':)'])).toBe(4);
    expect(kata.countSmileys([':)',':(',':D',':O',':;'])).toBe(2);
    expect(kata.countSmileys([';]', ':[', ';*', ':$', ';-D'])).toBe(1);
});

test('arrayDiff_twoArrays_firstArrayElements-secondArrayElements', () => {
    expect(kata.arrayDiff([], [4,5])).toEqual([]);
    expect(kata.arrayDiff([3,4], [3])).toEqual([4]);
    expect(kata.arrayDiff([1,8,2], [])).toEqual([1,8,2]);
    expect(kata.arrayDiff([1,2,3], [1,2])).toEqual([3]);
    expect(kata.arrayDiff([1,2,2,2,3],[2])).toEqual([1,3]);
});

test('iqTest_listOfNumbersWhereOneDiffInEvenness_indexOfThisOne+1', () => {
    expect(kata.iqTest("2 4 7 8 10")).toBe(3);
    expect(kata.iqTest("1 2 2")).toBe(1);
});

test('filterList_listWithStringsAndIntegers_listWithStringsFilteredOut', () => {
    expect(kata.filterList([1,2,'a','b'])).toEqual([1,2]);
    expect(kata.filterList([1,'a','b',0,15])).toEqual([1,0,15]);
    expect(kata.filterList([1,2,'aasf','1','123',123])).toEqual([1,2,123]);
});

test('uniqueInOrder_stringContainedDuplicatesElementsFollowedOneAfterTheOther_stringWithoutDuplicatesElementsFollowedOneAfterTheOther', () => {
    expect(kata.uniqueInOrder('AAAABBBCCDAABBB')).toEqual(['A', 'B', 'C', 'D', 'A', 'B']);
    expect(kata.uniqueInOrder('ABBCcAD')).toEqual(['A', 'B', 'C', 'c', 'A', 'D']);
    expect(kata.uniqueInOrder([1,2,2,3,3])).toEqual([1,2,3]);
});

test('test', () => {
    expect(kata.test([1,2,3,4])).toBeTruthy();
});