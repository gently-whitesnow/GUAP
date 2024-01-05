.model small
.stack 100
.data
        SG db "Hello world!",0DH, 0AH, '$'
.code
        MAIN PROC FAR
                MOV AX, @data
                MOV DS, AX
                LEA DX, SG
                MOV AH, 9
                INT 21H
                
                MOV AH, 4CH
                INT 21H
        MAIN ENDP
END MAIN
