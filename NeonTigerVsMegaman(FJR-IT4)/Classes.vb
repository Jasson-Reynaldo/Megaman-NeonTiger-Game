Imports System.IO

Public Enum StateNeonTiger
    Intro
    Stand
    JumpStart
    JumpUpForward
    JumpDown
    Landing
    ClingToWall
    RaySplashWall
    DiveAtt
    FallDown
    RaySplashGround
    ShotBlock
    RushAttGround
    RushAttAir
    MeleeCombo
    JumpDownR
    JumpDownM
    LandingR
End Enum

Public Enum FaceDir
    Left
    Right
End Enum
Public Enum StateTigerProjectile
    Create
End Enum
Public Enum StateMegaman
    Intro
    Stand
    Walk
    Jump
    JumpDown
    Shoot
    WalkShoot
    JumpShoot
    Electrocute
    Death
End Enum
Public Enum StateMegamanProjectile
    Create
End Enum
Public Class CImage
    Public Width As Integer
    Public Height As Integer
    Public Elmt(,) As System.Drawing.Color
    Public ColorMode As Integer 'not used

    Sub OpenImage(ByVal FName As String)
        Dim s As String
        Dim L As Long
        Dim BR As BinaryReader
        Dim h, w, pos As Integer
        Dim r, g, b As Integer
        Dim pad As Integer

        BR = New BinaryReader(File.Open(FName, FileMode.Open))

        Try
            BlockRead(BR, 2, s)

            If s <> "BM" Then
                MsgBox("Not a BMP file")
            Else 'BMP file
                BlockReadInt(BR, 4, L) 'size
                'MsgBox("Size = " + CStr(L))
                BlankRead(BR, 4) 'reserved
                BlockReadInt(BR, 4, pos) 'start of data
                BlankRead(BR, 4) 'size of header
                BlockReadInt(BR, 4, Width) 'width
                'MsgBox("Width = " + CStr(I.Width))
                BlockReadInt(BR, 4, Height) 'height
                'MsgBox("Height = " + CStr(I.Height))
                BlankRead(BR, 2) 'color panels
                BlockReadInt(BR, 2, ColorMode) 'colormode
                If ColorMode <> 24 Then
                    MsgBox("Not a 24-bit color BMP")
                Else

                    BlankRead(BR, pos - 30)

                    ReDim Elmt(Width - 1, Height - 1)
                    pad = (4 - (Width * 3 Mod 4)) Mod 4

                    For h = Height - 1 To 0 Step -1
                        For w = 0 To Width - 1
                            BlockReadInt(BR, 1, b)
                            BlockReadInt(BR, 1, g)
                            BlockReadInt(BR, 1, r)
                            Elmt(w, h) = Color.FromArgb(r, g, b)

                        Next
                        BlankRead(BR, pad)

                    Next

                End If

            End If

        Catch ex As Exception
            MsgBox("Error")

        End Try

        BR.Close()


    End Sub


    Sub CreateMask(ByRef Mask As CImage)
        Dim i, j As Integer

        Mask = New CImage
        Mask.Width = Width
        Mask.Height = Height

        ReDim Mask.Elmt(Mask.Width - 1, Mask.Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                If Elmt(i, j).R = 0 And Elmt(i, j).G = 0 And Elmt(i, j).B = 0 Then
                    Mask.Elmt(i, j) = Color.FromArgb(255, 255, 255)
                Else
                    Mask.Elmt(i, j) = Color.FromArgb(0, 0, 0)
                End If
            Next
        Next

    End Sub


    Sub CopyImg(ByRef Img As CImage)
        'copies image to Img
        Img = New CImage
        Img.Width = Width
        Img.Height = Height
        ReDim Img.Elmt(Width - 1, Height - 1)

        For i = 0 To Width - 1
            For j = 0 To Height - 1
                Img.Elmt(i, j) = Elmt(i, j)
            Next
        Next

    End Sub

End Class
Public Class CCharacter
    Public PosX, PosY As Double
    Public Vx, Vy As Double
    Public FrameIdx As Integer
    Public CurrFrame As Integer
    Public ArrSprites() As CArrFrame
    Public IdxArrSprites As Integer
    Public FDir As FaceDir
    Public Destroy As Boolean = False
    Public HitLeft, HitRight, HitTop, HitBottom As Integer
    Public Invulnerable As Boolean = True
    Public count As Integer = 0


    Public Const gravity = 1

    'Public CurrState as ?

    Public Sub GetNextFrame()
        CurrFrame = CurrFrame + 1
        If CurrFrame = ArrSprites(IdxArrSprites).Elmt(FrameIdx).MaxFrameTime Then
            FrameIdx = FrameIdx + 1
            If FrameIdx = ArrSprites(IdxArrSprites).N Then
                FrameIdx = 0
            End If
            CurrFrame = 0

        End If

    End Sub

    Public Overridable Sub Update()

    End Sub


End Class
Public Class CCharNeonTiger
    Inherits CCharacter

    Public CurrState As StateNeonTiger

    Public Sub State(state As StateNeonTiger, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()
        If FDir = FaceDir.Right Then
            HitLeft = PosX + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitright)
            HitRight = PosX - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitleft - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x)
        Else
            HitLeft = PosX - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitleft)
            HitRight = PosX + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitright - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x)
        End If
        HitTop = PosY - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hittop)
        HitBottom = PosY + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitbottom - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y)
        Select Case CurrState
            Case StateNeonTiger.Intro
                PosY += Vy
                GetNextFrame()
                If PosY < 325 Then
                    If FrameIdx = 1 And CurrFrame = 1 Then
                        FrameIdx = 0
                    End If
                End If

                If PosY >= 325 Then
                    PosY = 325
                    Vy = 0
                    If FrameIdx = 7 And CurrFrame = 1 And count < 4 Then
                        FrameIdx = 6
                        CurrFrame = 0
                        count += 1
                    End If
                    If FrameIdx = 0 And CurrFrame = 0 Then
                        State(StateNeonTiger.Stand, 1)
                        Vx = 0
                        Vy = 0
                    End If
                End If
            Case StateNeonTiger.Stand
                Invulnerable = False
                count = 0
                GetNextFrame()
            Case StateNeonTiger.JumpStart
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateNeonTiger.JumpUpForward, 3)
                    Vx = -5
                    Vy = -3
                End If
            Case StateNeonTiger.JumpUpForward
                Select Case FDir
                    Case FaceDir.Right
                        Vy = -3
                        Vx = 5
                        If PosX >= 430 Then
                            State(StateNeonTiger.ClingToWall, 6)
                            FDir = FaceDir.Left
                            Vy = 0
                            Vx = 0
                        End If
                    Case FaceDir.Left
                        Vx = -5
                        Vy = -3
                        If PosX <= 75 Then
                            State(StateNeonTiger.ClingToWall, 6)
                            FDir = FaceDir.Right
                            Vy = 0
                            Vx = 0
                        End If
                End Select
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If PosY <= 95 Then
                    State(StateNeonTiger.JumpDown, 4)
                    Vy = 3
                    Vx = 5
                End If
            Case StateNeonTiger.JumpDown
                Select Case FDir
                    Case FaceDir.Right
                        Vy = 3
                        Vx = 5
                    Case FaceDir.Left
                        Vx = -5
                        Vy = 3
                End Select
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If PosX <= 70 Then
                    State(StateNeonTiger.ClingToWall, 6)
                    FDir = FaceDir.Right
                    Vy = 0
                    Vx = 0
                End If
                If PosX >= 430 Then
                    State(StateNeonTiger.ClingToWall, 6)
                    FDir = FaceDir.Left
                    Vy = 0
                    Vx = 0
                End If
                If PosY >= 325 Then
                    State(StateNeonTiger.Landing, 5)
                    Vx = 0
                    Vy = 0
                End If

            Case StateNeonTiger.Landing
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateNeonTiger.Stand, 1)
                End If
            Case StateNeonTiger.ClingToWall
                GetNextFrame()
            Case StateNeonTiger.RaySplashWall
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateNeonTiger.ClingToWall, 6)
                    Vx = 0
                    Vy = 0
                End If
            Case StateNeonTiger.DiveAtt
                PosY += Vy
                Select Case FDir
                    Case FaceDir.Left
                        PosX -= Vx
                    Case FaceDir.Right
                        PosX += Vx
                End Select
                GetNextFrame()
                If PosY < 325 Then
                    If FrameIdx = 1 And CurrFrame = 1 Then
                        FrameIdx = 0
                    End If
                End If

                If PosY >= 325 Then
                    PosY = 325
                    Vx = 0
                    Vy = 0
                    If FrameIdx = 0 And CurrFrame = 0 Then
                        State(StateNeonTiger.Stand, 1)
                    End If
                End If
            Case StateNeonTiger.FallDown
                Invulnerable = True
                GetNextFrame()
                If CurrFrame = 2 And FrameIdx = 1 And count < 3 Then
                    CurrFrame = 0
                    FrameIdx = 0
                    count += 1
                End If
                If PosY < 325 Then
                    Vy = 5
                    PosY += Vy
                End If
                If PosY >= 325 Then
                    If FrameIdx = 0 And CurrFrame = 0 And count = 3 Then
                        State(StateNeonTiger.Stand, 1)
                        Vy = 0
                        Vx = 0
                    End If
                End If
            Case StateNeonTiger.RaySplashGround
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateNeonTiger.Stand, 1)
                End If
            Case StateNeonTiger.ShotBlock
                GetNextFrame()
                If FrameIdx = 2 And CurrFrame = 4 Then
                    CurrFrame = 0
                    FrameIdx = 2
                End If
            Case StateNeonTiger.RushAttGround
                Invulnerable = True
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If FrameIdx = 12 And CurrFrame = 1 Then
                    FrameIdx = 11
                End If
                Select Case FDir
                    Case FaceDir.Left
                        If FrameIdx = 11 Then
                            Vx = -5
                        End If
                        If PosX <= 70 Then
                            State(StateNeonTiger.RushAttAir, 13)
                            Vy = 0
                            Vx = 0
                        End If
                    Case FaceDir.Right
                        If FrameIdx = 11 Then
                            Vx = 5
                        End If
                        If PosX >= 430 Then
                            State(StateNeonTiger.RushAttAir, 13)
                            Vy = 0
                            Vx = 0
                        End If
                End Select

            Case StateNeonTiger.RushAttAir
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If FrameIdx = 1 Then
                    Vy = -5
                End If
                If FrameIdx = 2 And CurrFrame = 1 Then
                    FrameIdx = 1
                End If
                If PosY <= 95 Then
                    State(StateNeonTiger.JumpDownR, 15)
                    Vx = 0
                    Vy = 5
                End If
            Case StateNeonTiger.MeleeCombo
                PosY += Vy
                GetNextFrame()
                If FrameIdx = 7 And CurrFrame = 2 Then
                    Vx = 0
                    Vy = -5
                End If
                If FrameIdx = 9 And CurrFrame = 3 Then
                    FrameIdx = 9
                    CurrFrame = 1
                End If
                If PosY <= 95 Then
                    State(StateNeonTiger.JumpDownM, 16)
                    Vx = 0
                    Vy = 5
                End If
            Case StateNeonTiger.JumpDownR
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If PosY >= 325 Then
                    State(StateNeonTiger.LandingR, 17)
                    Vx = 0
                    Vy = 0
                End If
            Case StateNeonTiger.JumpDownM
                PosY += Vy
                GetNextFrame()
                If PosY >= 325 Then
                    State(StateNeonTiger.LandingR, 17)
                    Vx = 0
                    Vy = 0
                End If
            Case StateNeonTiger.LandingR
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateNeonTiger.Stand, 1)
                    Select Case FDir
                        Case FaceDir.Right
                            FDir = FaceDir.Left
                        Case FaceDir.Left
                            FDir = FaceDir.Right
                    End Select
                End If
        End Select

    End Sub

End Class
Public Class CCharTigerProjectile
    Inherits CCharacter

    Public CurrState As StateTigerProjectile

    Public Sub State(state As StateTigerProjectile, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub

    Public Overrides Sub Update()
        HitLeft = PosX - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Left)
        HitRight = PosX + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Right - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x)
        HitTop = PosY - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Top)
        HitBottom = PosY + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Bottom - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y)
        Select Case CurrState
            Case StateTigerProjectile.Create
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If PosX > 510 Or PosX < -10 Or PosY > 510 Or PosY < -10 Then
                    Destroy = True
                End If
        End Select
    End Sub

End Class
Public Class CCharMegaman
    Inherits CCharacter
    'Megaman in sprite is facing right instead of left, so the facedir for megaman is reversed tehe
    Public CurrState As StateMegaman

    Public Sub State(state As StateMegaman, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub
    Public Overrides Sub Update()
        HitLeft = PosX - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitleft)
        HitRight = PosX + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitright - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x)
        HitTop = PosY - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hittop)
        HitBottom = PosY + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Hitbottom - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y)
        Select Case CurrState
            Case StateMegaman.Intro
                count = 0
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.Stand, 1)
                End If
            Case StateMegaman.Stand
                Invulnerable = False
                GetNextFrame()
            Case StateMegaman.Walk
                PosX += Vx
                If PosX <= 55 Then
                    PosX = 55
                ElseIf PosX >= 445 Then
                    PosX = 445
                End If
                GetNextFrame()
            Case StateMegaman.Jump
                PosX += Vx
                PosY += Vy
                If PosX <= 55 Then
                    PosX = 55
                ElseIf PosX >= 445 Then
                    PosX = 445
                End If
                GetNextFrame()
                Vy = Vy + gravity
                If Vy >= 0 Then
                    State(StateMegaman.JumpDown, 9)
                End If
            Case StateMegaman.JumpDown
                PosX += Vx
                PosY += Vy
                If PosX <= 55 Then
                    PosX = 55
                ElseIf PosX >= 445 Then
                    PosX = 445
                End If
                GetNextFrame()
                Vy = Vy + gravity
                If PosY >= 325 Then
                    Vx = 0
                    Vy = 0
                    State(StateMegaman.Stand, 1)
                End If
            Case StateMegaman.Shoot
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.Stand, 1)
                End If
            Case StateMegaman.WalkShoot
                PosX += Vx
                If PosX <= 55 Then
                    PosX = 55
                ElseIf PosX >= 445 Then
                    PosX = 445
                End If
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.Walk, 2)
                End If
            Case StateMegaman.JumpShoot
                PosX += Vx
                PosY += Vy
                If PosX <= 55 Then
                    PosX = 55
                ElseIf PosX >= 445 Then
                    PosX = 445
                End If
                GetNextFrame()
                Vy = Vy + gravity
                If FrameIdx = 0 And CurrFrame = 0 Then
                    If Vy >= 0 Then
                        State(StateMegaman.JumpDown, 9)
                    Else
                        State(StateMegaman.Jump, 3)
                    End If

                End If
                If PosY >= 325 Then
                    Vx = 0
                    Vy = 0
                    State(StateMegaman.Stand, 1)
                End If
            Case StateMegaman.Electrocute
                Invulnerable = True
                GetNextFrame()
                If FrameIdx = 0 And CurrFrame = 0 Then
                    State(StateMegaman.Death, 8)
                End If


            Case StateMegaman.Death
                Invulnerable = True
                GetNextFrame()
                If CurrFrame = 2 And FrameIdx = 1 And count < 4 Then
                    CurrFrame = 0
                    FrameIdx = 0
                    count += 1
                End If
                If PosY < 325 Then
                    Vy = 5
                    PosY += Vy
                End If
                If PosY >= 325 Then
                    PosY = 325
                    If FrameIdx = 0 And CurrFrame = 0 And count = 4 Then
                        Destroy = True
                    End If
                End If

        End Select
    End Sub
End Class
Public Class CCharMegaProjectile
    Inherits CCharacter

    Public CurrState As StateMegamanProjectile

    Public Sub State(state As StateMegamanProjectile, idxspr As Integer)
        CurrState = state
        IdxArrSprites = idxspr
        CurrFrame = 0
        FrameIdx = 0

    End Sub
    Public Overrides Sub Update()
        HitLeft = PosX - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Left)
        HitRight = PosX + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Right - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.x)
        HitTop = PosY - (ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y - ArrSprites(IdxArrSprites).Elmt(FrameIdx).Top)
        HitBottom = PosY + (ArrSprites(IdxArrSprites).Elmt(FrameIdx).Bottom - ArrSprites(IdxArrSprites).Elmt(FrameIdx).CtrPoint.y)
        Select Case CurrState
            Case StateMegamanProjectile.Create
                PosX += Vx
                PosY += Vy
                GetNextFrame()
                If PosX > 510 Or PosX < -10 Or PosY > 510 Or PosY < -10 Then
                    Destroy = True
                End If
        End Select
    End Sub
End Class
Public Class CElmtFrame
    Public CtrPoint As TPoint
    Public Top, Bottom, Left, Right, Hitleft, Hitright, Hittop, Hitbottom As Integer
    Public Idx As Integer
    Public MaxFrameTime As Integer

    Public Sub New(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer, hl As Integer, ht As Integer, hr As Integer, hb As Integer)
        CtrPoint.x = ctrx
        CtrPoint.y = ctry
        Top = t
        Bottom = b
        Left = l
        Right = r
        MaxFrameTime = mft
        Hitleft = hl
        Hitright = hr
        Hittop = ht
        Hitbottom = hb

    End Sub
End Class

Public Class CArrFrame
    Public N As Integer
    Public Elmt As CElmtFrame()

    Public Sub New()
        N = 0
        ReDim Elmt(-1)
    End Sub

    Public Overloads Sub Insert(E As CElmtFrame)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1
    End Sub

    Public Overloads Sub Insert(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer, hl As Integer, ht As Integer, hr As Integer, hb As Integer)
        Dim E As CElmtFrame
        E = New CElmtFrame(ctrx, ctry, l, t, r, b, mft, hl, ht, hr, hb)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1

    End Sub

    Public Overloads Sub Insert(ctrx As Integer, ctry As Integer, l As Integer, t As Integer, r As Integer, b As Integer, mft As Integer)
        Dim E As CElmtFrame
        E = New CElmtFrame(ctrx, ctry, l, t, r, b, mft, 0, 0, 0, 0)
        ReDim Preserve Elmt(N)
        Elmt(N) = E
        N = N + 1
    End Sub
End Class

Public Structure TPoint
    Dim x As Integer
    Dim y As Integer

End Structure

