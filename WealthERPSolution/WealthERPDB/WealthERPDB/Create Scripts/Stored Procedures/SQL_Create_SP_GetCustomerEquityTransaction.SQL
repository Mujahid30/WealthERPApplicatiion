/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquityTransaction]    Script Date: 06/24/2009 11:59:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquityTransaction]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerEquityTransaction]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerEquityTransaction]    Script Date: 06/24/2009 11:59:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerEquityTransaction]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE  SP_GetCustomerEquityTransaction

@CET_EqTransId INT

AS

SELECT * FROM dbo.CustomerEquityTransaction WHERE  CET_EqTransId=@CET_EqTransId

' 
END
GO
