/****** Object:  StoredProcedure [dbo].[SP_Get_EqutiyAggregateValue]    Script Date: 06/24/2009 11:57:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Get_EqutiyAggregateValue]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_Get_EqutiyAggregateValue]
GO
/****** Object:  StoredProcedure [dbo].[SP_Get_EqutiyAggregateValue]    Script Date: 06/24/2009 11:57:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Get_EqutiyAggregateValue]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_Get_EqutiyAggregateValue

@A_AdviserId INT

AS

SELECT SUM(CENP_NetHoldings * CENP_MarketPrice) "Aggregate"  FROM dbo.ViewEquityNP AS VENP, dbo.AdviserBranch AS AB WHERE VENP.A_AdviserId=AB.A_AdviserId
' 
END
GO
