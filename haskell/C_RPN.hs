module C_RPN where

import Foreign.C.String
import Foreign.C.Types
import Data.List
import Control.Exception as E

foreign export ccall
    solveRPN_hs :: CString -> IO CFloat

solveRPN_hs :: CString -> IO CFloat
solveRPN_hs c_str = do
    str <- peekCString c_str
    let res = solveRPN str
    E.catch (return $ realToFrac $ solveRPN str) ignore

ignore :: E.SomeException -> IO CFloat
ignore _ = return 0

solveRPN :: String -> Float
solveRPN = head . foldl foldingFunction [] . words
    where   foldingFunction (x:y:ys) "*" = (x * y):ys
            foldingFunction (x:y:ys) "+" = (x + y):ys
            foldingFunction (x:y:ys) "-" = (y - x):ys
            foldingFunction (x:y:ys) "/" = (y / x):ys
            foldingFunction (x:y:ys) "^" = (y ** x):ys
            foldingFunction (x:xs) "ln" = log x:xs
            foldingFunction xs "sum" = [sum xs]
            foldingFunction xs numberString = read numberString:xs

-- compile: ghc --make -shared .\C_RPN.hs
