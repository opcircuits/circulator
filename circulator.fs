#10 constant #steps  ( -- u )


variable step#  ( -- a-addr )  0 step# !


create ext-data-in  ( -- a-addr )  0 c,


variable tlo-enable  ( -- a-addr )  false tlo-enable !


variable thi-enable  ( -- a-addr )  false thi-enable !


create tlo  ( -- a-addr )  0 c,


create thi  ( -- a-addr )  0 c,


: alu  ( -- x )  $a5 ;  \ Stubbed out


: header-row.  ( -- )
  ." Step" 3 spaces ." THI" 2 spaces ." TLO" cr ;


: table-line.  ( -- )  #15 0 do $2500 xemit loop cr ;


: state-row.  ( -- )
  step# @ 4 u.r 4 spaces  \ Display step number
  hex                     \ Switch to hexadecimal
  thi c@ u. 2 spaces      \ Display THI
  tlo c@ u. cr            \ Display TLO
  decimal ;               \ Switch to decimal


: run-step  ( -- )
  tlo-enable @ if          \ If TLOE is asserted
  alu tlo c! then          \ Update TLO
  thi-enable @ if          \ If THIE is asserted
  alu thi c! then          \ Update THI
  state-row.               \ Display state row
  step# dup @ 1+ swap ! ;  \ Increment step number


: emulate  ( -- )
  true tlo-enable !
  true thi-enable !
  header-row. table-line.
  #steps 0 do run-step loop ;  \ Run the emulation


cr emulate .s cr cr
