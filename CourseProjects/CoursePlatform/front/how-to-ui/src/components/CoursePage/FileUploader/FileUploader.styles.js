import styled from "styled-components";

export const FileUploaderWrapper = styled.div`
  .chous {
    padding: 5px;
    /* word-wrap: normal; word-break: break-all; */
    box-sizing: border-box;
    width: 180px;
    height: 60px;
    text-align: center;
    cursor: pointer;
    display: block;
    font: 14px/50px Tahoma;
    transition: all 0.18s ease-in-out;
    border: 1px solid black;
    background-color: white;
  }

  .chous:hover {
    background-color: ${(props) => props.color};
    color: white;
  }

  .my {
    width: 0.1px;
    height: 0.1px;
    opacity: 0;
    overflow: hidden;
    position: absolute;
    z-index: -1;
  }
`;
