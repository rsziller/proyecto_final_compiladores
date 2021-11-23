
bterm : '|' bfactor
      ;
bfactor : '~' bfactor
        | 'true'
        | 'false'
        ;
