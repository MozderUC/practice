var kata = require('./kata.js');

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