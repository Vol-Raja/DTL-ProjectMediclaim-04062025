USE [DTLPension]
GO

/****** Object:  Table [dbo].[Feedback]    Script Date: 25-02-2023 7.00.09 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Feedback](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Details] [nvarchar](max) NULL,
	 
	 
	[EmailAddress] [nvarchar](250) NULL,
	 
	[PhoneNumber] [nvarchar](250) NULL,
	[IsDeleted] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_ID]  DEFAULT (newid()) FOR [ID]
GO

ALTER TABLE [dbo].[Feedback] ADD  CONSTRAINT [DF_Feedback_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


