UPDATE Products
SET CategoryID = @TargetCategoryId
WHERE CategoryID = @SourceCategoryId