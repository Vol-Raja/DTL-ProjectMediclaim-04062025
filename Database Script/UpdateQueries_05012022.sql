
USE [DTLPension]
GO
ALTER TABLE dbo.Dispensary ADD OtherAmount MONEY NULL

USE [DTLPension]
GO
ALTER TABLE dbo.IPD ADD OtherAmount MONEY NULL

USE [DTLPension]
GO
ALTER TABLE dbo.OPDCND ADD OtherAmount MONEY NULL


USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveDispensary]    Script Date: 05-01-2022 17:31:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveDispensary]
(	
	@DispensaryDate date,
	@DispensaryLocation nvarchar(50),
	@DoctorVisited nvarchar(30),
	@TokenOPDNo nvarchar(30),
	@MedicineAmount money,
	@Investigation nvarchar(150),
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30),
	@OtherAmount money = null
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[Dispensary]
	           ([DispensaryDate]
			   ,[DispensaryLocation]
	           ,[DoctorVisited]
	           ,[TokenOPDNo]
	           ,[MedicineAmount]
	           ,[Investigation]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy]
			   ,[OtherAmount])
	     VALUES
	           (@DispensaryDate
			   ,@DispensaryLocation
	           ,@DoctorVisited
	           ,@TokenOPDNo
	           ,@MedicineAmount
	           ,@Investigation
	           ,@TotalAmount
	           ,@NonCashlessClaimId
	           ,@CreatedBy
			   ,@OtherAmount)
COMMIT TRAN
	
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


USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveIPD]    Script Date: 05-01-2022 17:34:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveIPD]
(	
	@DateOfAddmission date,
	@HospitalName nvarchar(40),
	@TreatmentIllsness nvarchar(150),
	@DateOfDischarge date,
	@TotalAmount money,
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30), 
	@OtherAmount money = null
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[IPD]
	           ([DateOfAddmission]
	           ,[HospitalName]
	           ,[TreatmentIllsness]
	           ,[DateOfDischarge]
	           ,[TotalAmount]
	           ,[NonCashlessClaimId]
	           ,[CreatedBy]
			   ,[OtherAmount])
	     VALUES
	           (@DateOfAddmission
	           ,@HospitalName
	           ,@TreatmentIllsness
	           ,@DateOfDischarge
	           ,@TotalAmount
	           ,@NonCashlessClaimId
	           ,@CreatedBy
			   ,@OtherAmount)
COMMIT TRAN
	
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

USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[SaveOPDCND]    Script Date: 05-01-2022 17:36:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SaveOPDCND]
(	
	@OPDCNDDate date,
	@HospitalName nvarchar(40),
	@MedicineAmount money,
	@InvestigationAmount money,
	@ConsultationAmount money,
	@TotalAmount money,
	@HospitalizationClaimType nchar(3),
	@NonCashlessClaimId int,
	@CreatedBy nvarchar(30),
	@OtherAmount money = null
)
AS
BEGIN TRAN 
BEGIN TRY
	INSERT INTO [dbo].[OPDCND]
           ([OPDCNDDate]
           ,[HospitalName]
           ,[MedicineAmount]
           ,[InvestigationAmount]
           ,[ConsultationAmount]
           ,[TotalAmount]
           ,[HospitalizationClaimType]
           ,[NonCashlessClaimId]
           ,[CreatedBy]
		   ,[OtherAmount])
     VALUES
           (@OPDCNDDate
           ,@HospitalName
           ,@MedicineAmount
           ,@InvestigationAmount 
           ,@ConsultationAmount
           ,@TotalAmount
           ,@HospitalizationClaimType
           ,@NonCashlessClaimId
           ,@CreatedBy
		   ,@OtherAmount)
COMMIT TRAN
	
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



USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[UpdateDispensary]    Script Date: 05-01-2022 19:14:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateDispensary]
(
	@DispensaryId INT,
	@DispensaryDate DATE,
	@DispensaryLocation NVARCHAR(50),
	@DoctorVisited NVARCHAR(30),
	@TokenOPDNo NVARCHAR(30),
	@MedicineAmount MONEY,
	@Investigation NVARCHAR(150),
	@TotalAmount MONEY,
	@NonCashlessClaimId INT,
	@ModifiedBy NVARCHAR(30),
	@OtherAmount MONEY
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[Dispensary] 
		SET DispensaryDate = @DispensaryDate
		,DispensaryLocation = @DispensaryLocation
		,DoctorVisited = @DoctorVisited
		,TokenOPDNo = @TokenOPDNo
		,MedicineAmount = @MedicineAmount
		,Investigation = @Investigation
		,TotalAmount = @TotalAmount
		,NonCashlessClaimId = @NonCashlessClaimId
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
		,OtherAmount = @OtherAmount
		WHERE 
			DispensaryId = @DispensaryId

	COMMIT TRAN  
	
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


USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[UpdateIPD]    Script Date: 05-01-2022 19:16:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UpdateIPD]
(
	@IPDId INT,
	@DateOfAddmission date,
	@HospitalName nvarchar(40),
	@TreatmentIllsness nvarchar(150),
	@DateOfDischarge date,
	@TotalAmount money,
	@NonCashlessClaimId int,
	@ModifiedBy NVARCHAR(50),
	@OtherAmount MONEY
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[IPD] 
		SET DateOfAddmission = @DateOfAddmission
		,HospitalName = @HospitalName
		,TreatmentIllsness = @TreatmentIllsness
		,DateOfDischarge = @DateOfDischarge
		,TotalAmount = @TotalAmount
		,NonCashlessClaimId = @NonCashlessClaimId
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
		,OtherAmount = @OtherAmount
		WHERE 
			IPDId = @IPDId

	COMMIT TRAN  
	
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
