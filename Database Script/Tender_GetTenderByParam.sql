
CREATE PROCEDURE [dbo].[GetTenderByParam]          
(          
 @TenderId  uniqueidentifier = NULL,          
 @IsArchieved bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID]
      ,[TitleInEnglish]
      ,[TitleInHindi]
      ,[TenderIdInHindi]
      ,[TenderIdEnglish]
      ,[OpeningDate]
      ,[OpeningTime]
      ,[PublishDate]
      ,[PublishTime]
      ,[ClosingDate]
      ,[ClosingTime]
      ,[DiscriptionInEnglish]
      ,[DiscriptionInHindi]
      ,[AttachmentTitleInEnglish]
      ,[AttachmentTitleInHindi]
      ,[AttachmentFileInEnglish]
      ,[AttachmentFileInHindi]
      ,[IsDeleted]
      ,[IsArchieved]
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[Tender]
       
  WHERE           
   ((ID = @TenderId) OR @TenderId IS NULL)          
   AND ((IsArchieved = @IsArchieved) OR @IsArchieved IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   