USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[AddEditTestimony]    Script Date: 25-02-2023 7.51.07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[AddEditTestimony]
    @Id               uniqueidentifier            = null,
    @NameEnglish      nvarchar(250)  = NULL,
    @NameHindi        nvarchar(250)  = NULL,
    @PositionEnglish  nvarchar(250)  = NULL,
    @PositionHindi    nvarchar(250)  = NULL,
	@Description		nvarchar(max) = NULL,
	@DescriptionHindi nvarchar (max) =NULL,
    @IsDeleted        bit            = NULL,
    @CreatedDate      datetime       = NULL,
    @ModifiedDate     datetime       = NULL,
    @CreatedBy        nvarchar(250)  = NULL,
    @ModifiedBy       nvarchar(250)  = NULL,
    @Image            varbinary(max) = NULL,
    @ImageName        nvarchar(250)  = NULL,
    @ImageContentType nvarchar(250)  = NULL,
	
    @ReturnMsg        nvarchar(max)  output
AS
    BEGIN

        begin try

            if (@Id is null)
                begin

                    INSERT INTO Testimony
                        (
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
                        )
                    VALUES
                        (
                            @NameEnglish,
                            @NameHindi,
                            @PositionEnglish,
                            @PositionHindi,
							@Description,
							@DescriptionHindi,
                            @IsDeleted,
						 
                            @Image,
                            @ImageName,
                            @ImageContentType,
                            @CreatedBy,
                            @ModifiedBy
                        )
                    set @ReturnMsg = 'add'
                end
            else
                begin

                    Update
                        Testimony
                    set
                        NameEnglish = @NameEnglish,
                        NameHindi = @NameHindi,
                        PositionEnglish = @PositionEnglish,
                        PositionHindi = @PositionHindi,
						Description =@Description,
						DescriptionHindi =@DescriptionHindi,
                        IsDeleted = @IsDeleted,
						 
                        Image = @Image,
                        ImageContentType = @ImageContentType,
                        ImageName = @ImageName,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = GETDATE()
                    where
                        Id = @Id
                    set @ReturnMsg = 'update'
                end
        end try
        begin catch
            set @ReturnMsg = ERROR_MESSAGE()

        end catch
    END

GO


