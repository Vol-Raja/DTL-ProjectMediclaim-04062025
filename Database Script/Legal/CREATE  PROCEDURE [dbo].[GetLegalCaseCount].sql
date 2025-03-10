ALTER  PROCEDURE [dbo].[GetLegalCaseCount]          
      ( @Count int output)  
AS          
BEGIN          
          
  set @Count = (SELECT COUNT(*)    
  FROM [dbo].[LegalCases])
  RETURN @Count   
END   
  