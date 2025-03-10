
GO
alter table dbo.Grievance 
add Reply nvarchar(200) null

GO

CREATE PROCEDURE [dbo].[GrievanceReplyMessage]  
(  
 @Id uniqueidentifier,  
 @Reply [NVARCHAR](200),
 @ModifiedBy [NVARCHAR](60)
)  
AS  
BEGIN TRAN  
BEGIN TRY  

  UPDATE dbo.Grievance
	  SET Reply = @Reply, 
	  ModifideBy = @ModifiedBy,
	  ModifideDate = GETDATE()
	WHERE Id = @Id

  COMMIT TRAN  

END TRY  
BEGIN CATCH  
 ROLLBACK TRAN  
 DECLARE @ErrorMessage NVARCHAR(4000);    
    DECLARE @ErrorSeverity INT;    
    DECLARE @ErrorState INT;    
    
    SELECT     
        @ErrorMessage = ERROR_MESSAGE(),    
        @ErrorSeverity = ERROR_SEVERITY(),    
        @ErrorState = ERROR_STATE();    
    
    -- Use RAISERROR inside the CATCH block to return error    
    -- information about the original error that caused    
    -- execution to jump to the CATCH block.    
    RAISERROR (@ErrorMessage, -- Message text.    
               @ErrorSeverity, -- Severity.    
               @ErrorState -- State.    
               );    
END CATCH  

--------------------------
--LEGALCASES
--------------------------

GO
ALTER TABLE dbo.LegalCases 
ADD Approve BIT null

GO
ALTER TABLE dbo.LegalCases
ADD CONSTRAINT DF_LegalCases_Approve
DEFAULT 0 FOR Approve;

GO
UPDATE dbo.LegalCases SET Approve = 0

GO
ALTER TABLE dbo.LegalCases 
ADD IsDeleted BIT null

GO
ALTER TABLE dbo.LegalCases
ADD CONSTRAINT DF_LegalCases_IsDeleted
DEFAULT 0 FOR IsDeleted;

GO
UPDATE dbo.LegalCases SET IsDeleted = 0

GO
CREATE PROCEDURE [dbo].[ApproveLegalCases]  
(  
 @Id uniqueidentifier,
 @ModifiedBy NVARCHAR(60)
)  
AS  
BEGIN TRAN  
BEGIN TRY  

  UPDATE dbo.LegalCases
	  SET Approve = 1, 
	  ModifiedBy = @ModifiedBy,
	  ModifiedDate = GETDATE()
	WHERE ID = @Id

  COMMIT TRAN  

END TRY  
BEGIN CATCH  
 ROLLBACK TRAN  
 DECLARE @ErrorMessage NVARCHAR(4000);    
    DECLARE @ErrorSeverity INT;    
    DECLARE @ErrorState INT;    
    
    SELECT     
        @ErrorMessage = ERROR_MESSAGE(),    
        @ErrorSeverity = ERROR_SEVERITY(),    
        @ErrorState = ERROR_STATE();    
    
    -- Use RAISERROR inside the CATCH block to return error    
    -- information about the original error that caused    
    -- execution to jump to the CATCH block.    
    RAISERROR (@ErrorMessage, -- Message text.    
               @ErrorSeverity, -- Severity.    
               @ErrorState -- State.    
               );    
END CATCH  

GO
  
ALTER PROCEDURE [dbo].[usp_LegalCases_GetAll]    
AS  
BEGIN  
  
SELECT ID  
,CaseNo  
,CourtType  
,FileNumber  
,CaseInitialDate  
,NextHearingDate  
,PartiesInvolved  
,NatureOfCase  
,SummaryOfCase  
,NameOfCouncil  
,SubjectOfCase  
,CaseEndDate  
,CaseResult  
,CreatedDate  
,ModifiedDate  
,CreatedBy  
,ModifiedBy
,Approve
,IsDeleted
FROM LegalCases  
  
END 

GO
CREATE PROCEDURE [dbo].[DeleteLegalCases]  
(  
 @Id uniqueidentifier,
 @ModifiedBy NVARCHAR(60)
)  
AS  
BEGIN TRAN  
BEGIN TRY  

  UPDATE dbo.LegalCases
	  SET IsDeleted = 1, 
	  ModifiedBy = @ModifiedBy,
	  ModifiedDate = GETDATE()
	WHERE ID = @Id

  COMMIT TRAN  

END TRY  
BEGIN CATCH  
 ROLLBACK TRAN  
 DECLARE @ErrorMessage NVARCHAR(4000);    
    DECLARE @ErrorSeverity INT;    
    DECLARE @ErrorState INT;    
    
    SELECT     
        @ErrorMessage = ERROR_MESSAGE(),    
        @ErrorSeverity = ERROR_SEVERITY(),    
        @ErrorState = ERROR_STATE();    
    
    -- Use RAISERROR inside the CATCH block to return error    
    -- information about the original error that caused    
    -- execution to jump to the CATCH block.    
    RAISERROR (@ErrorMessage, -- Message text.    
               @ErrorSeverity, -- Severity.    
               @ErrorState -- State.    
               );    
END CATCH 