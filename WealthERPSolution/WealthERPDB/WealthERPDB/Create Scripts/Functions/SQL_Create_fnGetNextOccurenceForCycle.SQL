/****** Object:  UserDefinedFunction [dbo].[fnGetNextOccurenceForCycle]    Script Date: 06/24/2009 15:30:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetNextOccurenceForCycle]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnGetNextOccurenceForCycle]
GO
/****** Object:  UserDefinedFunction [dbo].[fnGetNextOccurenceForCycle]    Script Date: 06/24/2009 15:30:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnGetNextOccurenceForCycle]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
Create FUNCTION [dbo].[fnGetNextOccurenceForCycle](@prevOccurence DateTime, @cycleID int)
RETURNS datetime
AS
BEGIN

-- Declaration of Varoiables
declare @nextOccurence DateTime -- This is the new occurence date

SET @nextOccurence = CASE   
    WHEN @cycleID = 2 THEN DATEADD(hh,24,@prevOccurence)
    WHEN @cycleID = 3 THEN DATEADD(wk,1,@prevOccurence)  
    WHEN @cycleID = 4 THEN DATEADD(hh,84,@prevOccurence)
    WHEN @cycleID = 5 THEN DATEADD(m,1,@prevOccurence)
	WHEN @cycleID = 6 THEN DATEADD(d,15,@prevOccurence)
	WHEN @cycleID = 7 THEN DATEADD(m,3,@prevOccurence)
	WHEN @cycleID = 8 THEN DATEADD(m,6,@prevOccurence)
	WHEN @cycleID = 9 THEN DATEADD(yyyy,1,@prevOccurence)
	END

--This is a sanity check on the Next Occurence. Suppose the service does not run for a few days
-- then in that case the next occurence will always be in the past. So do a check where if the CALCULATED next occurence
-- is in the past then set it to the beginning of the NEXT DAY
IF (dbo.GetStartOfDay(@nextOccurence) < dbo.GetStartOfDay(GETDATE()))
BEGIN
	SET @nextOccurence =  dbo.GetStartOfDay(DATEADD(d,1,GETDATE()))
END

RETURN @nextOccurence
END 




--select dbo.fnGetNextOccurenceForCycle(GETDATE(),9)' 
END

GO
