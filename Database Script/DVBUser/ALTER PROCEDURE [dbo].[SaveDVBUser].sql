USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveDVBUser]    Script Date: 24-09-2022 8.45.51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveDVBUser]
(
	@ID UNIQUEIDENTIFIER,
	@Name NVARCHAR(60),
	@EmailAddress NVARCHAR(120),
	@PhoneNumber NVARCHAR(20),
	@Department NVARCHAR(100),
	@Designation NVARCHAR(50),
	@CreatedBy NVARCHAR(60),
	@DVBUserId INT OUTPUT,
	@Address nvarchar(500),
	@Username nvarchar(255),
	@Dashboard nvarchar(255),
	@DashboardUrl nvarchar(500)
)
As
BEGIN TRAN
BEGIN TRY 

	INSERT INTO [dbo].[DVBUser]
			([ID]
			,[Name]
			,[EmailAddress]	
			,[PhoneNumber]
			,[Department]
			,[Designation]
			,[CreatedBy]
			,[Address]  
			,Username  
			,Dashboard  
			,DashboardUrl
			)
     VALUES
           (@ID
		    ,@Name
			,@EmailAddress
			,@PhoneNumber
			,@Department
			,@Designation
			,@CreatedBy
			,@Address  
			,@Username  
			,@Dashboard  
			,@DashboardUrl  
			)
	COMMIT TRAN  

	SET @DVBUserId = SCOPE_IDENTITY();
	
END TRY
BEGIN CATCH
   ROLLBACK TRAN
    -- Raise an error and return
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
