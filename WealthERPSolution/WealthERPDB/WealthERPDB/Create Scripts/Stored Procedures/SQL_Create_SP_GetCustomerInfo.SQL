/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInfo]    Script Date: 06/24/2009 12:00:13 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerInfo]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerInfo]    Script Date: 06/24/2009 12:00:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerInfo]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerInfo]
@U_UserId bigint
as
select * from Customer where U_UMId=@U_UserId
' 
END
GO
