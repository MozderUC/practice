var kata = require('./kata.js');

test('iqTest_listOfNumbersWhereOneDiffInEvenness_indexOfThisNumber+1', () => {
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