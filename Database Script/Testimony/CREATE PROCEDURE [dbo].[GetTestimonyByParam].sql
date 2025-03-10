USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[GetTestimonyByParam]    Script Date: 25-02-2023 7.55.05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetTestimonyByParam]
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
			Description,
			DescriptionHindi,
            IsDeleted,
		 
            Image,
            ImageName,
            ImageContentType,
            CreatedBy,
            ModifiedBy
        FROM
            [DTLPension].[dbo].[Testimony]
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
GO


