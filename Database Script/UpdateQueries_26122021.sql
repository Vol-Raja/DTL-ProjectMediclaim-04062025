
-----------------ALTER Queries----------------
-------------------Start----------------------
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  Active BIT 
GO
ALTER TABLE [dbo].[MediclaimNonCashless] ADD  CONSTRAINT [DF_MediclaimNonCashless_Active]  DEFAULT ((1)) FOR [Active]
GO


CREATE PROCEDURE [dbo].[UpdateMediclaimNonCashless]
(
	@ClaimId INT,
	@EmployeeNumber NVARCHAR(15),
	@PPONumber NVARCHAR(10),
	@MedicalSectionPageNumber INT,	
	@MedicalCardHolderName NVARCHAR(50),
	@Designation NVARCHAR(50),
	@PatientName NVARCHAr(50),
	@Gender NVARCHAR(6),
	@DateOfBirth DATE,
	@Age INT,
	@ClaimFor NVARCHAR(9),
	@RelationWithRetire NVARCHAR(8) NULL,
	@MobileNumber NVARCHAR(10),
	@CardCategory NVARCHAR(20),
	@Address NVARCHAR(150),
	@ClaimType NVARCHAR(3),
	@MedicalCardPhotoCopy BIT,
	@PrescriptionDetailPhotoCopy BIT,
	@OriginalBill BIT,
	@CashMemo BIT,
	@ClaimStatusId BIT,
	@ModifiedBy NVARCHAR(50) NULL
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[MediclaimNonCashless] 
		SET
		EmployeeNumber = @EmployeeNumber
		,PPONumber = @PPONumber
		,MedicalSectionPageNumber = @MedicalSectionPageNumber	
		,MedicalCardHolderName = @MedicalCardHolderName
		,Designation = @Designation
		,PatientName = @PatientName
		,Gender = @Gender
		,DateOfBirth =  @DateOfBirth
		,Age =  @Age
		,ClaimFor = @ClaimFor
		,RelationWithRetire = @RelationWithRetire
		,MobileNumber = @MobileNumber
		,CardCategory = @CardCategory
		,[Address] = @Address
		,ClaimType = @ClaimType
		,MedicalCardPhotoCopy = @MedicalCardPhotoCopy
		,PrescriptionDetailPhotoCopy = @PrescriptionDetailPhotoCopy
		,OriginalBill = @OriginalBill
		,CashMemo =  @CashMemo
		,@ClaimStatusId =  1
		,RejectReason = NULL
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
		WHERE 
			ClaimId = @ClaimId

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


-----------------Update Queries----------------
-----------------------------------------------
CREATE PROCEDURE [dbo].[UpdateDispensary]
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
	@ModifiedBy NVARCHAR(30)
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



CREATE PROCEDURE [dbo].[UpdateIPD]
(
	@IPDId INT,
	@DateOfAddmission date,
	@HospitalName nvarchar(40),
	@TreatmentIllsness nvarchar(150),
	@DateOfDischarge date,
	@TotalAmount money,
	@NonCashlessClaimId int,
	@ModifiedBy NVARCHAR(50)
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



CREATE PROCEDURE [dbo].[UpdateOPDCND]
(
	@OPDCNDId INT,
	@OPDCNDDate DATE,
	@HospitalName NVARCHAR(40),
	@MedicineAmount MONEY,
	@InvestigationAmount MONEY,
	@ConsultationAmount MONEY,
	@TotalAmount MONEY,
	@HospitalizationClaimType NCHAR(3),
	@NonCashlessClaimId INT,
	@ModifiedBy NVARCHAR(50)
)
As
BEGIN TRAN
BEGIN TRY 

	UPDATE [dbo].[OPDCND] 
		SET OPDCNDDate = @OPDCNDDate
		,HospitalName = @HospitalName
		,MedicineAmount = @MedicineAmount
		,InvestigationAmount = @InvestigationAmount
		,ConsultationAmount = @ConsultationAmount
		,TotalAmount = @TotalAmount
		,NonCashlessClaimId = @NonCashlessClaimId
		,ModifiedBy = @ModifiedBy
		,ModifiedDate = GETDATE()
		WHERE 
			OPDCNDId = @OPDCNDId

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
