GO
ALTER TABLE AspNetUsers 
ADD Designation NVARCHAR(60) NULL

GO
ALTER TABLE dbo.MediclaimCashless
ADD ForwardTo NVARCHAR(60) NULL


GO
ALTER TABLE dbo.MediclaimCashless
ADD DealingAssistanceRemark NVARCHAR(250) NULL

GO
ALTER TABLE dbo.MediclaimCashless
ADD ASORemark NVARCHAR(250) NULL

GO
ALTER TABLE dbo.MediclaimCashless
ADD SORemark NVARCHAR(250) NULL


GO
ALTER TABLE dbo.MediclaimCashless
ADD ApproveRemark NVARCHAR(250) NULL

GO  
ALTER PROCEDURE [dbo].[GetPendingCashlessClaims]      
AS      
BEGIN      
 SELECT [dbo].[MediclaimCashless].[ClaimId],      
 [dbo].[MediclaimCashless].[NameOfHospital],      
 [dbo].[MediclaimCashless].[HospitalPhoneNumber],      
 [dbo].[MediclaimCashless].[HospitalAddress],      
 [dbo].[MediclaimCashless].[HospitalId],      
 [dbo].[MediclaimCashless].[EmailId],      
 [dbo].[MediclaimCashless].[NameOfPatient],   
 [dbo].[MediclaimCashless].[PatientEmailId],  
 [dbo].[MediclaimCashless].[Gender],      
 [dbo].[MediclaimCashless].[PatientPhoneNumber],      
 [dbo].[MediclaimCashless].[PatientAddress],      
 [dbo].[MediclaimCashless].[DateOfBirth],      
 [dbo].[MediclaimCashless].[Age],      
 [dbo].[MediclaimCashless].[MedicalSectionPageNumber],      
 [dbo].[MediclaimCashless].[NameOfCardHolder],      
 [dbo].[MediclaimCashless].[MedicalCardNumber],      
 [dbo].[MediclaimCashless].[AdmissionNumber],      
 [dbo].[MediclaimCashless].[CardCategory],      
 [dbo].[MediclaimCashless].[CaseType],      
 [dbo].[MediclaimCashless].[TypeOfTreatment],      
 [dbo].[MediclaimCashless].[Amount],      
 [dbo].[MediclaimCashless].[DateOfAdmission],      
 [dbo].[MediclaimCashless].[DateOfDischargeOrDeath],   
 [dbo].[MediclaimCashless].[ClaimStatusId],      
 [dbo].[MediclaimCashless].[RejectReason],      
 [dbo].[MediclaimCashless].[PhysicalSubmit],      
[dbo].[MediclaimCashless].[ApprovedAmount],      
[dbo].[MediclaimCashless].[CreatedBy],      
[dbo].[MediclaimCashless].[CreatedDate],      
[dbo].[MediclaimCashless].[ModifiedBy],      
[dbo].[MediclaimCashless].[ModifiedDate],  
[dbo].[MediclaimCashless].[AccountHolderName],  
[dbo].[MediclaimCashless].[AccountNumber],  
[dbo].[MediclaimCashless].[BankName],  
[dbo].[MediclaimCashless].[BICCode],  
[dbo].[MediclaimCashless].[IFSCNumber],  
 [dbo].[MediclaimCashless].[BranchName],
 [dbo].[MediclaimCashless].[ForwardTo],
 [dbo].[MediclaimCashless].[DealingAssistanceRemark],
 [dbo].[MediclaimCashless].[ASORemark],
 [dbo].[MediclaimCashless].[SORemark],
 [dbo].[MediclaimCashless].[ApproveRemark],
 [dbo].[MediclaimVoucher].[VoucherId]
  FROM [dbo].[MediclaimCashless] LEFT JOIN[dbo].[MediclaimVoucher]
  ON [dbo].[MediclaimCashless].ClaimId = [dbo].[MediclaimVoucher].ClaimId
  WHERE ClaimStatusId IN (1,2,3)      
END   
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[GetCashlessClaimByParam](
	@Date Date = NULL,
	@ClaimId INT = NULL,
	@PageNumber INT = NULL 
)
AS
BEGIN
	SELECT [dbo].[MediclaimCashless].[ClaimId],      
 [dbo].[MediclaimCashless].[NameOfHospital],      
 [dbo].[MediclaimCashless].[HospitalPhoneNumber],      
 [dbo].[MediclaimCashless].[HospitalAddress],      
 [dbo].[MediclaimCashless].[HospitalId],      
 [dbo].[MediclaimCashless].[EmailId],      
 [dbo].[MediclaimCashless].[NameOfPatient],   
 [dbo].[MediclaimCashless].[PatientEmailId],  
 [dbo].[MediclaimCashless].[Gender],      
 [dbo].[MediclaimCashless].[PatientPhoneNumber],      
 [dbo].[MediclaimCashless].[PatientAddress],      
 [dbo].[MediclaimCashless].[DateOfBirth],      
 [dbo].[MediclaimCashless].[Age],      
 [dbo].[MediclaimCashless].[MedicalSectionPageNumber],      
 [dbo].[MediclaimCashless].[NameOfCardHolder],      
 [dbo].[MediclaimCashless].[MedicalCardNumber],      
 [dbo].[MediclaimCashless].[AdmissionNumber],      
 [dbo].[MediclaimCashless].[CardCategory],      
 [dbo].[MediclaimCashless].[CaseType],      
 [dbo].[MediclaimCashless].[TypeOfTreatment],      
 [dbo].[MediclaimCashless].[Amount],      
 [dbo].[MediclaimCashless].[DateOfAdmission],      
 [dbo].[MediclaimCashless].[DateOfDischargeOrDeath],   
 [dbo].[MediclaimCashless].[ClaimStatusId],      
 [dbo].[MediclaimCashless].[RejectReason],      
 [dbo].[MediclaimCashless].[PhysicalSubmit],      
[dbo].[MediclaimCashless].[ApprovedAmount],      
[dbo].[MediclaimCashless].[CreatedBy],      
[dbo].[MediclaimCashless].[CreatedDate],      
[dbo].[MediclaimCashless].[ModifiedBy],      
[dbo].[MediclaimCashless].[ModifiedDate],  
[dbo].[MediclaimCashless].[AccountHolderName],  
[dbo].[MediclaimCashless].[AccountNumber],  
[dbo].[MediclaimCashless].[BankName],  
[dbo].[MediclaimCashless].[BICCode],  
[dbo].[MediclaimCashless].[IFSCNumber],  
 [dbo].[MediclaimCashless].[BranchName],
 [dbo].[MediclaimCashless].[ForwardTo],
  [dbo].[MediclaimCashless].[DealingAssistanceRemark],
 [dbo].[MediclaimCashless].[ASORemark],
 [dbo].[MediclaimCashless].[SORemark],
 [dbo].[MediclaimCashless].[ApproveRemark],
 [dbo].[MediclaimVoucher].[VoucherId]
  FROM [dbo].[MediclaimCashless] LEFT JOIN[dbo].[MediclaimVoucher]
  ON [dbo].[MediclaimCashless].ClaimId = [dbo].[MediclaimVoucher].ClaimId
  WHERE 
	(CONVERT(DATE,[dbo].[MediclaimCashless].[CreatedDate],102) = (CONVERT(DATE,@DATE,102)) OR @Date IS NULL)
	AND (([dbo].[MediclaimCashless].ClaimId = @ClaimId) OR @ClaimId IS NULL)
	AND ((MedicalSectionPageNumber = @PageNumber) OR @PageNumber IS NULL)
	AND ([dbo].[MediclaimCashless].IsDelete = 0)
END
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetCashlessListByClaimId]
(
	@ClaimId int
)
AS
BEGIN
SELECT [dbo].[MediclaimCashless].[ClaimId],      
 [dbo].[MediclaimCashless].[NameOfHospital],      
 [dbo].[MediclaimCashless].[HospitalPhoneNumber],      
 [dbo].[MediclaimCashless].[HospitalAddress],      
 [dbo].[MediclaimCashless].[HospitalId],      
 [dbo].[MediclaimCashless].[EmailId],      
 [dbo].[MediclaimCashless].[NameOfPatient],   
 [dbo].[MediclaimCashless].[PatientEmailId],  
 [dbo].[MediclaimCashless].[Gender],      
 [dbo].[MediclaimCashless].[PatientPhoneNumber],      
 [dbo].[MediclaimCashless].[PatientAddress],      
 [dbo].[MediclaimCashless].[DateOfBirth],      
 [dbo].[MediclaimCashless].[Age],      
 [dbo].[MediclaimCashless].[MedicalSectionPageNumber],      
 [dbo].[MediclaimCashless].[NameOfCardHolder],      
 [dbo].[MediclaimCashless].[MedicalCardNumber],      
 [dbo].[MediclaimCashless].[AdmissionNumber],      
 [dbo].[MediclaimCashless].[CardCategory],      
 [dbo].[MediclaimCashless].[CaseType],      
 [dbo].[MediclaimCashless].[TypeOfTreatment],      
 [dbo].[MediclaimCashless].[Amount],      
 [dbo].[MediclaimCashless].[DateOfAdmission],      
 [dbo].[MediclaimCashless].[DateOfDischargeOrDeath],   
 [dbo].[MediclaimCashless].[ClaimStatusId],      
 [dbo].[MediclaimCashless].[RejectReason],      
 [dbo].[MediclaimCashless].[PhysicalSubmit],      
[dbo].[MediclaimCashless].[ApprovedAmount],      
[dbo].[MediclaimCashless].[CreatedBy],      
[dbo].[MediclaimCashless].[CreatedDate],      
[dbo].[MediclaimCashless].[ModifiedBy],      
[dbo].[MediclaimCashless].[ModifiedDate],  
[dbo].[MediclaimCashless].[AccountHolderName],  
[dbo].[MediclaimCashless].[AccountNumber],  
[dbo].[MediclaimCashless].[BankName],  
[dbo].[MediclaimCashless].[BICCode],  
[dbo].[MediclaimCashless].[IFSCNumber],  
 [dbo].[MediclaimCashless].[BranchName],
 [dbo].[MediclaimCashless].[ForwardTo],
  [dbo].[MediclaimCashless].[DealingAssistanceRemark],
 [dbo].[MediclaimCashless].[ASORemark],
 [dbo].[MediclaimCashless].[SORemark],
 [dbo].[MediclaimCashless].[ApproveRemark],
 [dbo].[MediclaimVoucher].[VoucherId]
  FROM [dbo].[MediclaimCashless] LEFT JOIN[dbo].[MediclaimVoucher]
  ON [dbo].[MediclaimCashless].ClaimId = [dbo].[MediclaimVoucher].ClaimId
  WHERE
	[dbo].[MediclaimCashless].[ClaimId] = @ClaimId AND
	[dbo].[MediclaimCashless].[IsDelete] = 0
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[GetCashlessListByCreatedBy]
(
	@CreatedBy nvarchar(50)
)
AS
BEGIN
SELECT  [dbo].[MediclaimCashless].[ClaimId],      
 [dbo].[MediclaimCashless].[NameOfHospital],      
 [dbo].[MediclaimCashless].[HospitalPhoneNumber],      
 [dbo].[MediclaimCashless].[HospitalAddress],      
 [dbo].[MediclaimCashless].[HospitalId],      
 [dbo].[MediclaimCashless].[EmailId],      
 [dbo].[MediclaimCashless].[NameOfPatient],   
 [dbo].[MediclaimCashless].[PatientEmailId],  
 [dbo].[MediclaimCashless].[Gender],      
 [dbo].[MediclaimCashless].[PatientPhoneNumber],      
 [dbo].[MediclaimCashless].[PatientAddress],      
 [dbo].[MediclaimCashless].[DateOfBirth],      
 [dbo].[MediclaimCashless].[Age],      
 [dbo].[MediclaimCashless].[MedicalSectionPageNumber],      
 [dbo].[MediclaimCashless].[NameOfCardHolder],      
 [dbo].[MediclaimCashless].[MedicalCardNumber],      
 [dbo].[MediclaimCashless].[AdmissionNumber],      
 [dbo].[MediclaimCashless].[CardCategory],      
 [dbo].[MediclaimCashless].[CaseType],      
 [dbo].[MediclaimCashless].[TypeOfTreatment],      
 [dbo].[MediclaimCashless].[Amount],      
 [dbo].[MediclaimCashless].[DateOfAdmission],      
 [dbo].[MediclaimCashless].[DateOfDischargeOrDeath],   
 [dbo].[MediclaimCashless].[ClaimStatusId],      
 [dbo].[MediclaimCashless].[RejectReason],      
 [dbo].[MediclaimCashless].[PhysicalSubmit],      
[dbo].[MediclaimCashless].[ApprovedAmount],      
[dbo].[MediclaimCashless].[CreatedBy],      
[dbo].[MediclaimCashless].[CreatedDate],      
[dbo].[MediclaimCashless].[ModifiedBy],      
[dbo].[MediclaimCashless].[ModifiedDate],  
[dbo].[MediclaimCashless].[AccountHolderName],  
[dbo].[MediclaimCashless].[AccountNumber],  
[dbo].[MediclaimCashless].[BankName],  
[dbo].[MediclaimCashless].[BICCode],  
[dbo].[MediclaimCashless].[IFSCNumber],  
 [dbo].[MediclaimCashless].[BranchName],
 [dbo].[MediclaimCashless].[ForwardTo],
  [dbo].[MediclaimCashless].[DealingAssistanceRemark],
 [dbo].[MediclaimCashless].[ASORemark],
 [dbo].[MediclaimCashless].[SORemark],
 [dbo].[MediclaimCashless].[ApproveRemark],
 [dbo].[MediclaimVoucher].[VoucherId]
  FROM [dbo].[MediclaimCashless] LEFT JOIN[dbo].[MediclaimVoucher]
  ON [dbo].[MediclaimCashless].ClaimId = [dbo].[MediclaimVoucher].ClaimId
  WHERE
	 [dbo].[MediclaimCashless].[CreatedBy] = @CreatedBy AND
	 [dbo].[MediclaimCashless].[IsDelete] = 0
END
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[ApproveRejectCashlessClaim](
	@ClaimId INT,
	@ClaimStatusId INT,
	@RejectReason NVARCHAR(200) = NULL,
	@ApprovedAmount MONEY,
	@ApproveRemark NVARCHAR(250),
	@DealingAssistanceRemark NVARCHAR(250), 
	@ASORemark  NVARCHAR(250),
	@SORemark NVARCHAR(250),
	@ModifiedBy  NVARCHAR(50)
)
AS
BEGIN TRAN
BEGIN TRY 

	UPDATE dbo.MediclaimCashless 
		SET ClaimStatusId = @ClaimStatusId,
		RejectReason = @RejectReason,
		ApprovedAmount =@ApprovedAmount,
		ApproveRemark = @ApproveRemark,
		DealingAssistanceRemark = @DealingAssistanceRemark, 
		ASORemark = @ASORemark,
		SORemark = @SORemark,
		ModifiedBy = @ModifiedBy,
		ModifiedDate = GETUTCDATE()
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
GO
