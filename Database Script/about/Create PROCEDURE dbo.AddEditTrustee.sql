USE DTLPension
GO
/****** Object:  StoredProcedure dbo.AddEditTrustee    Script Date: 22-09-2022 8.14.00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



alter PROCEDURE dbo.AddEditTrustee
    @Id               uniqueidentifier            = null,
    @NameEnglish      nvarchar(250)  = NULL,
    @NameHindi        nvarchar(250)  = NULL,
    @PositionEnglish  nvarchar(250)  = NULL,
    @PositionHindi    nvarchar(250)  = NULL,
    @IsDeleted        bit            = NULL,
    @CreatedDate      datetime       = NULL,
    @ModifiedDate     datetime       = NULL,
    @CreatedBy        nvarchar(250)  = NULL,
    @ModifiedBy       nvarchar(250)  = NULL,
    @Image            varbinary(max) = NULL,
    @ImageName        nvarchar(250)  = NULL,
    @ImageContentType nvarchar(250)  = NULL,
	@phone nvarchar(25)  = NULL,
    @ReturnMsg        nvarchar(max)  output
AS
    BEGIN

        begin try

            if (@Id is null)
                begin

                    INSERT INTO Trustee
                        (
                            NameEnglish,
                            NameHindi,
                            PositionEnglish,
                            PositionHindi,
                            IsDeleted,
							Phone,
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
                            @IsDeleted,
							@phone,
                            @Image,
                            @ImageName,
                            @ImageContentType,
                            @CreatedBy,
                            @ModifiedBy
                        )
                    set @ReturnMsg = SCOPE_IDENTITY()
                end
            else
                begin

                    Update
                        Trustee
                    set
                        NameEnglish = @NameEnglish,
                        NameHindi = @NameHindi,
                        PositionEnglish = @PositionEnglish,
                        PositionHindi = @PositionHindi,
                        IsDeleted = @IsDeleted,
						phone = @phone,
                        Image = @Image,
                        ImageContentType = @ImageContentType,
                        ImageName = @ImageName,
                        ModifiedBy = @ModifiedBy,
                        ModifiedDate = GETDATE()
                    where
                        Id = @Id
                    set @ReturnMsg = SCOPE_IDENTITY()
                end
        end try
        begin catch
            set @ReturnMsg = ERROR_MESSAGE()

        end catch
    END

