
CREATE PROCEDURE [dbo].[GetLinkByParam]          
(          
 @LinkId  uniqueidentifier = NULL,          
 @IsArchieved bit = NULL,          
 @IsDeleted bit = NULL
    
)          
AS          
BEGIN          
          
  SELECT             
  [ID],
      Title ,
	DocumentFileName  ,
	ContentType  ,
	FileSize  ,
	FileContent 
	
      ,[IsDeleted]
      ,[IsArchieved]
      ,[CreatedDate]
      ,[ModifiedDate]
      ,[CreatedBy]
      ,[ModifiedBy]
  FROM [DTLPension].[dbo].[Link]
       
  WHERE           
   ((ID = @LinkId) OR @LinkId IS NULL)          
   AND ((IsArchieved = @IsArchieved) OR @IsArchieved IS NULL)          
   AND ((IsDeleted = @IsDeleted) OR @IsDeleted IS NULL)          
     
END   