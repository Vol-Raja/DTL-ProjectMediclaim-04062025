USE [DTLPension]
GO
/****** Object:  StoredProcedure [dbo].[GetTrusteeByParam]    Script Date: 22-09-2022 10.30.08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

alter PROCEDURE [dbo].[GetTrusteeByParam]
    (
        @Id        uniqueidentifier = NULL,
        @IsDeleted bit = NULL
    )
AS
    BEGIN

        SELECT
            [ID] ,
			NameEnglish,
            NameHindi,
            PositionEnglish,
            PositionHindi,
            IsDeleted,
			phone,
            Image,
            ImageName,
            ImageContentType,
            CreatedBy,
            ModifiedBy
        FROM
            [DTLPension].[dbo].[Trustee]
        WHERE
            (
                (ID = @Id)
                OR @Id IS NULL
            )
            AND (
                    (IsDeleted = @IsDeleted)
                    OR @IsDeleted IS NULL
                )

    END
