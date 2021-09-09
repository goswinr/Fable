module Sample.AST

open Fable.Transforms.Rust.AST.Adapters
open Fable.Transforms.Rust.AST.Spans
open Fable.Transforms.Rust.AST.Types
open Fable.Transforms.Rust.AST.Helpers

// The AST for this Rust program:
//
// fn main() {
//   let a = vec![1,2,3,4,5];
//   println!("{:?}", a);
// }

let stmt1 =
  ["1";"2";"3";"4";"5"]
  |> Seq.map mkIntToken
  |> mkBracketCommaDelimitedMacCall "vec"
  |> mkMacCallExpr
  |> mkIdentLocal "a"
  |> mkLocalStmt

let stmt2 =
  [ mkStrToken "{:?}"; mkIdentToken "a" ]
  |> mkParensCommaDelimitedMacCall "println"
  |> mkMacCallStmt

let test_crate: Crate = {
  attrs = mkVec []
  items = mkVec [
    [stmt1; stmt2] |> mkBlock |> Some
    |> mkFnKind DEFAULT_FN_HEADER (mkFnDecl NO_PARAMS VOID_RETURN_TY) NO_GENERICS
    |> mkFnItem (mkVec []) "main"
  ]
  span = DUMMY_SP
  proc_macros = mkVec []
}
