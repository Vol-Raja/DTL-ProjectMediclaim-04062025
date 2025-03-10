USE [DTLPension]
GO

/****** Object:  StoredProcedure [dbo].[[GetVisitor]]    Script Date: 02-04-2023 6.02.17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetVisitor]
AS
BEGIN
	SELECT (
			SELECT COUNT(*) CurrentMonthVisitorCount
			FROM [DTLPension].[dbo].Visitor
			WHERE VisitDate >= datefromparts(year(getdate()), month(getdate()), 1)
			) CurrentMonthVisitorCount
		,(
			SELECT COUNT(*) TotalVisitorCount
			FROM [DTLPension].[dbo].Visitor
			) TotalVisitorCount
		,(
			SELECT COUNT(*) TodayVisitorCount
			FROM [DTLPension].[dbo].Visitor
			WHERE VisitDate >= datefromparts(year(getdate()), month(getdate()), day(getdate()))
			) TodayVisitorCount
END
GO


