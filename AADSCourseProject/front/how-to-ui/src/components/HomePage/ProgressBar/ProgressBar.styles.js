import styled, { keyframes } from 'styled-components';

export const Pie = styled.div`
  --p: ${props => props.percentage};
  --b: ${props => props.borderWidth};
  --c: ${props => props.color};
  --w: 50px;

  width: var(--w);
  aspect-ratio: 1;
  position: relative;
  display: inline-grid;
  margin: 5px;
  place-content: center;
  font-size: 12px;
  font-weight: bold;
  font-family: sans-serif;

  &:before,
  &:after {
    content: "";
    position: absolute;
    border-radius: 50%;
  }

  &:before {
    inset: 0;
    background:
      radial-gradient(farthest-side, var(--c) 98%, #0000) top/var(--b) var(--b) no-repeat,
      conic-gradient(var(--c) calc(var(--p) * 1%), #0000 0);
    -webkit-mask: radial-gradient(farthest-side, #0000 calc(99% - var(--b)), #000 calc(100% - var(--b)));
            mask: radial-gradient(farthest-side, #0000 calc(99% - var(--b)), #000 calc(100% - var(--b)));
  }

  &:after {
    inset: calc(50% - var(--b) / 2);
    background: var(--c);
    transform: rotate(calc(var(--p) * 3.6deg)) translateY(calc(50% - var(--w) / 2));
  }

  &.no-round:before {
    background-size: 0 0, auto;
  }

  &.no-round:after {
    content: none;
  }
`;
